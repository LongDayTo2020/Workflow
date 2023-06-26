using Workflow.Repository.DBEntity;

namespace Workflow.Repository.Interface;

public interface IWorkflowRecordRepository
{
    IEnumerable<WorkflowRecord> Query();

    WorkflowRecord? Create(WorkflowRecord workflow);

    bool Update(WorkflowRecord workflow);

    bool Delete(WorkflowRecord workflow);
}