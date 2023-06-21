namespace Workflow.Repository.Interface;

public interface IWorkflowRepository
{
    IEnumerable<DBEntity.Workflow> Query();

    bool Create(DBEntity.Workflow workflow);

    bool Update(DBEntity.Workflow workflow);

    bool Delete(DBEntity.Workflow workflow);
}