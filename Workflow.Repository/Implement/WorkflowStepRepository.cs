using System.Data;
using Dapper;
using Workflow.Repository.DBEntity;
using Workflow.Repository.Interface;

namespace Workflow.Repository.Implement;

public class WorkflowStepRepository : IWorkflowStepRepository
{
    private readonly IDbConnection _dbConnection;

    public WorkflowStepRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public IEnumerable<WorkflowStep> Query()
    {
        string sql = @"select * from workflow_steps ";
        return _dbConnection.Query<WorkflowStep>(sql);
    }

    public bool Create(WorkflowStep workflowStep)
    {
        string sql = @"
insert into workflow_steps (workflow_id, step, final, name, create_time, create_user)
values (
@WorkflowId,
@Step,
@Final,
@Name,
@CreateTime,
@CreateUser );
";
        return _dbConnection.Execute(sql, workflowStep) > 0;
    }

    public bool Update(WorkflowStep workflowStep)
    {
        string sql = @"
update workflow_steps
set   
    step = @Step,
    final = @Final,
    name = @Name,
    update_time = @UpdateTime ,
    update_user = @UpdateUser 
where id = @Id AND workflow_id = @WorkflowId;
";
        return _dbConnection.Execute(sql, workflowStep) > 0;
    }

    public bool Delete(WorkflowStep workflowStep)
    {
        string sql = @"delete workflow_steps where id = @Id AND workflow_id = @WorkflowId;";
        return _dbConnection.Execute(sql, workflowStep) > 0;
    }
}