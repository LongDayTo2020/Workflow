using System.Data;
using Microsoft.Extensions.Hosting;
using Workflow.Library;
using Workflow.Repository.DBEntity;
using Workflow.Repository.Enums;
using Workflow.Repository.Interface;
using Workflow.Service.Dto.Param;
using Workflow.Service.Dto.Result;
using Workflow.Service.Interface;

namespace Workflow.Service.Implement;

public class ApplicationProcedureService : IApplicationProcedureService
{
    private readonly IWorkflowRecordRepository _workflowRecordRepository;
    private readonly IWorkflowStepRepository _workflowStepRepository;
    private readonly IWorkflowApprovalRepository _workflowApprovalRepository;
    private readonly IWorkflowRecordFileRepository _workflowRecordFileRepository;
    private readonly IDbConnection _dbConnection;
    private readonly IHostEnvironment _hostingEnvironment;
    private readonly string _filePath;


    public ApplicationProcedureService(IWorkflowRecordRepository workflowRecordRepository,
        IWorkflowApprovalRepository workflowApprovalRepository, IDbConnection dbConnection,
        IWorkflowStepRepository workflowStepRepository, IHostEnvironment hostingEnvironment,
        IWorkflowRecordFileRepository workflowRecordFileRepository)
    {
        _workflowRecordRepository = workflowRecordRepository;
        _workflowApprovalRepository = workflowApprovalRepository;
        _dbConnection = dbConnection;
        _workflowStepRepository = workflowStepRepository;
        _hostingEnvironment = hostingEnvironment;
        _workflowRecordFileRepository = workflowRecordFileRepository;
        _filePath = "RecordFile";
    }

    public List<ResultApplicationProcedure> GetAll()
    {
        var result = _workflowRecordRepository.Query()
            .Join(_workflowApprovalRepository.Query(),
                t1 => t1.Id,
                t2 => t2.WorkflowRecordId,
                (t1, t2) => new { Table1 = t1, Table2 = t2 })
            .GroupBy(x => x.Table1, x => x.Table2)
            .Select(q =>
            {
                var t = new ResultApplicationProcedure();
                ObjectLibrary.CloneProperties(q.Key, t);
                t.ApprovalInfoList = q.ToList().Select(t2 =>
                {
                    var a = new ApprovalInfo();
                    ObjectLibrary.CloneProperties(t2, a);
                    return a;
                }).ToList();
                return t;
            })
            .ToList();
        return result;
    }

    public ResultApplicationProcedure Get(int workflowRecordId)
    {
        var result = _workflowRecordRepository.Query().Where(x => x.Id == workflowRecordId)
            .Join(_workflowApprovalRepository.Query(),
                t1 => t1.Id,
                t2 => t2.WorkflowRecordId,
                (t1, t2) => new { Table1 = t1, Table2 = t2 })
            .GroupBy(x => x.Table1, x => x.Table2)
            .Select(q =>
            {
                var t = new ResultApplicationProcedure();
                ObjectLibrary.CloneProperties(q.Key, t);
                t.ApprovalInfoList = q.ToList().Select(t2 =>
                {
                    var a = new ApprovalInfo();
                    ObjectLibrary.CloneProperties(t2, a);
                    return a;
                }).ToList();
                return t;
            }).FirstOrDefault() ?? new ResultApplicationProcedure();
        return result;
    }

    public bool Add(ParamApplicationProcedure applicationProcedure)
    {
        bool result;
        var newRecord = new WorkflowRecord();
        ObjectLibrary.CloneProperties(applicationProcedure, newRecord);
        newRecord.Status = ProcedureStatus.WaitReview;
        using var transaction = _dbConnection.BeginTransaction();
        try
        {
            //新增流程資料
            newRecord = _workflowRecordRepository.Create(newRecord);
            if (newRecord == null) throw new Exception("新增失敗");
            //新增第一步資料
            var firstStep = _workflowStepRepository.Query().OrderBy(o => o.Step)
                .FirstOrDefault(x => x.WorkflowId == newRecord.WorkflowId);
            if (firstStep == null) throw new Exception("無第一步流程資料");
            var newStep = new WorkflowApproval()
            {
                WorkflowRecordId = newRecord.WorkflowId,
                WorkflowStepId = firstStep.Id,
                ExpirationTime = applicationProcedure.ExpirationTime,
                Approver = applicationProcedure.Approver,
                Description = applicationProcedure.Description,
                Status = ProcedureStatus.WaitReview
            };
            _workflowApprovalRepository.Create(newStep);
            transaction.Commit();
            result = true;
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }

        return result;
    }

    public bool Approval(int applicationProcedureId, ParamJudgment judgment)
    {
        bool result;
        var workflowApproval = _workflowApprovalRepository.Query()
            .Where(x => x.WorkflowRecordId == judgment.WorkflowRecordId)
            .MaxBy(o => o.WorkflowStepId);
        if (workflowApproval == null) throw new Exception("查無資料");
        var record = _workflowRecordRepository.Query().FirstOrDefault(x => x.Id == workflowApproval.WorkflowRecordId);
        if (record == null) throw new Exception("查無資料");
        var stepList = _workflowStepRepository.Query()
            .Where(x => x.WorkflowId == record.WorkflowId)
            .OrderBy(o => o.Step)
            .ToList();
        var nowStep = stepList.FirstOrDefault(x => x.Id == workflowApproval.WorkflowStepId);
        if (nowStep == null) throw new Exception("查無資料");
        var nextStep = stepList.SkipWhile(x => x.Id == workflowApproval.WorkflowStepId)
            .Skip(1)
            .FirstOrDefault(x => x.Id > workflowApproval.WorkflowStepId);
        if (nextStep == null) throw new Exception("查無資料");
        var transaction = _dbConnection.BeginTransaction();
        try
        {
            //同意目前的步驟
            workflowApproval.Status = ProcedureStatus.Approval;
            workflowApproval.ApprovalTime = DateTime.Now;
            _workflowApprovalRepository.Update(workflowApproval);
            //是否最後一步
            if (nowStep.Final > 0)
            {
                record.Status = ProcedureStatus.Finish;
                _workflowRecordRepository.Update(record);
            }
            else
            {
                //新增下一步
                var newApproval = new WorkflowApproval();
                ObjectLibrary.CloneProperties(judgment, newApproval);
                newApproval.WorkflowStepId = nextStep.Id;
                _workflowApprovalRepository.Create(newApproval);
            }

            transaction.Commit();
            result = true;
        }
        catch (Exception)
        {
            result = false;
            transaction.Rollback();
        }

        return result;
    }

    public bool Reject(int applicationProcedureId, ParamJudgment judgment)
    {
        bool result;
        var workflowApproval = _workflowApprovalRepository.Query()
            .Where(x => x.WorkflowRecordId == judgment.WorkflowRecordId)
            .MaxBy(o => o.WorkflowStepId);
        if (workflowApproval == null) throw new Exception("查無資料");
        var record = _workflowRecordRepository.Query().FirstOrDefault(x => x.Id == workflowApproval.WorkflowRecordId);
        if (record == null) throw new Exception("查無資料");
        var nowStep = _workflowStepRepository.Query()
            .FirstOrDefault(x => x.Id == workflowApproval.WorkflowStepId);
        var transaction = _dbConnection.BeginTransaction();
        try
        {
            //同意目前的步驟
            workflowApproval.Status = ProcedureStatus.Reject;
            workflowApproval.ApprovalTime = DateTime.Now;
            _workflowApprovalRepository.Update(workflowApproval);
            //更新流程狀態
            record.Status = ProcedureStatus.Reject;
            _workflowRecordRepository.Update(record);
            transaction.Commit();
            result = true;
        }
        catch (Exception)
        {
            result = false;
            transaction.Rollback();
        }

        return result;
    }

    public bool AddFile(ParamApplicationProcedureFile applicationProcedureFile)
    {
        var procedure = _workflowApprovalRepository.Query().FirstOrDefault(x =>
            x.WorkflowRecordId == applicationProcedureFile.WorkflowRecordId &&
            x.WorkflowStepId == applicationProcedureFile.WorkflowStepId);
        if (procedure == null) throw new Exception("查無步驟資料");
        var newWorkflowFile = new WorkflowRecordFile();
        ObjectLibrary.CloneProperties(applicationProcedureFile, newWorkflowFile);
        var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, _filePath,
            applicationProcedureFile.WorkflowRecordId.ToString(),
            applicationProcedureFile.WorkflowStepId.ToString());
        filePath = FileLibrary.SaveFileAsync(applicationProcedureFile.File, filePath).GetAwaiter().GetResult();
        newWorkflowFile.Length = applicationProcedureFile.File.Length;
        newWorkflowFile.Name = applicationProcedureFile.File.FileName;
        newWorkflowFile.Type = applicationProcedureFile.File.ContentType;
        newWorkflowFile.Location = filePath.Replace(_hostingEnvironment.ContentRootPath, string.Empty);
        return _workflowRecordFileRepository.Create(newWorkflowFile);
    }

    public bool DeleteFile(int applicationProcedureId, int applicationProcedureFileId)
    {
        var deleteWork = _workflowRecordFileRepository.Query().FirstOrDefault(x =>
            x.WorkflowRecordId == applicationProcedureId && x.Id == applicationProcedureFileId);
        if (deleteWork == null) throw new Exception("查無資料");
        return _workflowRecordFileRepository.Delete(deleteWork);
    }
}