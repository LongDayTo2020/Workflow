using Workflow.Repository.DBEntity;

namespace Workflow.Repository.Interface;

public interface IWorkflowApprovalRepository
{
    IEnumerable<WorkflowApproval> Query();

    bool Create(WorkflowApproval workflow);

    bool Update(WorkflowApproval workflow);

    bool Delete(WorkflowApproval workflow);
}