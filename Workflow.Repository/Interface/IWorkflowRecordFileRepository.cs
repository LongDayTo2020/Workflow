using Workflow.Repository.DBEntity;

namespace Workflow.Repository.Interface;

public interface IWorkflowRecordFileRepository
{
    IEnumerable<WorkflowRecordFile> Query();

    bool Create(WorkflowRecordFile workflow);

    bool Update(WorkflowRecordFile workflow);

    bool Delete(WorkflowRecordFile workflow);
}