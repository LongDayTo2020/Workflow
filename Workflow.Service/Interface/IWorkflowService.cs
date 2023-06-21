using Workflow.Service.Dto.Param;
using Workflow.Service.Dto.Result;

namespace Workflow.Service.Interface;

public interface IWorkflowService
{
    List<ResultWorkflow> GetAll();

    ResultWorkflow? Get(int workflowId);

    bool Add(ParamWorkflow workflow);

    bool Update(int workflowId, ParamWorkflow workflow);

    bool Delete(int workflowId);
}