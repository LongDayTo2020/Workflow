using Workflow.Repository.Interface;
using Workflow.Service.Dto.Param;
using Workflow.Service.Dto.Result;
using Workflow.Service.Interface;

namespace Workflow.Service.Implement;

public class ApplicationProcedureService : IApplicationProcedureService
{
    private readonly IWorkflowRecordRepository _workflowRecordRepository;
    private readonly IWorkflowRecordFileRepository _workflowRecordFileRepository;
    private readonly IWorkflowApprovalRepository _workflowApprovalRepository;

    public ApplicationProcedureService(IWorkflowRecordRepository workflowRecordRepository,
        IWorkflowRecordFileRepository workflowRecordFileRepository,
        IWorkflowApprovalRepository workflowApprovalRepository)
    {
        _workflowRecordRepository = workflowRecordRepository;
        _workflowRecordFileRepository = workflowRecordFileRepository;
        _workflowApprovalRepository = workflowApprovalRepository;
    }

    public List<ResultApplicationProcedure> GetAll()
    {
        throw new NotImplementedException();
    }

    public ResultApplicationProcedure Get(int workflowRecordId)
    {
        throw new NotImplementedException();
    }

    public bool Add(ParamApplicationProcedure applicationProcedure)
    {
        throw new NotImplementedException();
    }

    public bool Approval(int applicationProcedureId, ParamJudgment judgment)
    {
        throw new NotImplementedException();
    }

    public bool Reject(int applicationProcedureId, ParamJudgment judgment)
    {
        throw new NotImplementedException();
    }

    public bool AddFile(ParamApplicationProcedureFile applicationProcedureFile)
    {
        throw new NotImplementedException();
    }

    public bool DeleteFile(int applicationProcedureId, int applicationProcedureFileId)
    {
        throw new NotImplementedException();
    }
}