using System.Data;
using Dapper;
using Workflow.Repository.Interface;

namespace Workflow.Repository.Implement;

public class WorkflowRepository : IWorkflowRepository
{
    private readonly IDbConnection _dbConnection;

    public WorkflowRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public IEnumerable<DBEntity.Workflow> Query()
    {
        string sql = @"
select *
from workflows;
";
        return _dbConnection.Query<DBEntity.Workflow>(sql);
    }

    public bool Create(DBEntity.Workflow workflow)
    {
        string sql = @"
insert into workflows (name, create_time, create_user)
values (
@Name,
@CreateTime, 
@CreateUser );
";
        return _dbConnection.Execute(sql, workflow) > 0;
    }

    public bool Update(DBEntity.Workflow workflow)
    {
        string sql = @"
update workflows
set  
name = @Name,
update_time = @UpdateTime ,
update_user = @UpdateUser 
where id = @Id;
";
        return _dbConnection.Execute(sql, workflow) > 0;
    }

    public bool Delete(DBEntity.Workflow workflow)
    {
        string sql = @"
delete workflows where id = @Id;
";
        return _dbConnection.Execute(sql, workflow) > 0;
    }
}