using Workflow.Repository.DBEntity;

namespace Workflow.Repository.Interface;

public interface IWorkflowStepRepository
{
    IEnumerable<WorkflowStep> Query();

    bool Create(WorkflowStep workflowStep);

    bool Update(WorkflowStep workflowStep);

    bool Delete(WorkflowStep workflowStep);
}