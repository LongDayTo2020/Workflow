using Workflow.Repository.DBEntity;

namespace Workflow.Repository.Interface;

public interface IWorkflowStepRepository
{
    IEnumerable<WorkflowStep> Query();

    bool Create(WorkflowStep workflow);

    bool Update(WorkflowStep workflow);

    bool Delete(WorkflowStep workflow);
}