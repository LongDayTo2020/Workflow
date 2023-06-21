using Workflow.Service.Dto.Param;
using Workflow.Service.Dto.Result;

namespace Workflow.Service.Interface;

public interface IApplicationProcedureService
{
    List<ResultApplicationProcedure> GetAll();

    ResultApplicationProcedure Get(int workflowRecordId);

    bool Add(ParamApplicationProcedure applicationProcedure);

    bool Approval(int applicationProcedureId, ParamJudgment judgment);

    bool Reject(int applicationProcedureId, ParamJudgment judgment);

    bool AddFile(ParamApplicationProcedureFile applicationProcedureFile);

    bool DeleteFile(int applicationProcedureId, int applicationProcedureFileId);
}