using Workflow.Service.Dto.Param;
using Workflow.Service.Dto.Result;

namespace Workflow.Service.Interface;

public interface IWorkflowStepService
{
    List<ResultWorkflowStep> Get(int workflowId);

    bool Add(ParamWorkflowStep workflowStep);

    bool Update(int workflowStepId, ParamWorkflowStep workflowStep);

    bool Delete(int workflowStepId);
}