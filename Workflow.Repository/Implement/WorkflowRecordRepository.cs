using System.Data;
using Dapper;
using Workflow.Repository.DBEntity;
using Workflow.Repository.Interface;
using Workflow.Repository.Mapper;

namespace Workflow.Repository.Implement;

public class WorkflowRecordRepository : IWorkflowRecordRepository
{
    private readonly IDbConnection _dbConnection;

    public WorkflowRecordRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public IEnumerable<WorkflowRecord> Query()
    {
        string sql = @"
select *
from workflow_records;
";
        SqlMapper.SetTypeMap(typeof(WorkflowRecord), new CustomMapper<WorkflowRecord>());
        return _dbConnection.Query<WorkflowRecord>(sql);
    }

    public WorkflowRecord? Create(WorkflowRecord workflow)
    {
        workflow.CreateTime = DateTime.Now;
        string sql = @"
insert into workflow_records (workflow_id, priority, expiration_time, status, create_time, create_user)
values (
@WorkflowId, 
@Priority, 
@ExpirationTime, 
@Status, 
@CreateTime, 
@CreateUser  )
RETURNING *;
";
        SqlMapper.SetTypeMap(typeof(WorkflowRecord), new CustomMapper<WorkflowRecord>());
        return _dbConnection.Query<WorkflowRecord>(sql, workflow).FirstOrDefault();
    }

    public bool Update(WorkflowRecord workflow)
    {
        string sql = @"
update workflow_records
set  
priority = @Priority, 
expiration_time = @ExpirationTime, 
status = @Status,
update_time = @UpdateTime ,
update_user = @UpdateUser 
where id = @Id AND workflow_id = @WorkflowId;
";
        return _dbConnection.Execute(sql, workflow) > 0;
    }

    public bool Delete(WorkflowRecord workflow)
    {
        string sql = @"
delete
from workflow_records
where id = @Id AND workflow_id = @WorkflowId;
";
        return _dbConnection.Execute(sql, workflow) > 0;
    }
}