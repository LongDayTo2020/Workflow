using System.Data;
using Dapper;
using Workflow.Repository.DBEntity;
using Workflow.Repository.Interface;

namespace Workflow.Repository.Implement;

public class WorkflowRecordFileRepository : IWorkflowRecordFileRepository
{
    private readonly IDbConnection _dbConnection;

    public WorkflowRecordFileRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public IEnumerable<WorkflowRecordFile> Query()
    {
        string sql = @"
select *
from workflow_record_files;
";
        return _dbConnection.Query<WorkflowRecordFile>(sql);
    }

    public bool Create(WorkflowRecordFile workflow)
    {
        workflow.CreateTime = DateTime.Now;
        string sql = @"
insert into workflow_record_files (workflow_record_id, workflow_step_id, name, show_name, type, length, location,
        description, create_time, create_user)
values (
@WorkflowRecordId, 
@WorkflowStepId, 
@Name,  
@ShowName,  
@Type,  
@Length, 
@Location,  
@Description, 
@CreateTime, 
@CreateUser );
";
        return _dbConnection.Execute(sql, workflow) > 0;
    }

    public bool Update(WorkflowRecordFile workflow)
    {
        string sql = @"
update workflow_record_files
set 
    name = @Name,  
    show_name = @ShowName,  
    type = @Type,  
    length = @Length, 
    location = @Location,  
    description = @Description, 
    update_time = @UpdateTime ,
    update_user = @UpdateUser 
where 
    id = @Id 
    AND workflow_record_id = @WorkflowRecordId
    AND workflow_step_id = @WorkflowStepId ;
";
        return _dbConnection.Execute(sql, workflow) > 0;
    }

    public bool Delete(WorkflowRecordFile workflow)
    {
        string sql = @"delete workflow_record_files 
where id = @Id AND workflow_record_id = @WorkflowRecordId AND workflow_step_id = @WorkflowStepId ; ";
        return _dbConnection.Execute(sql, workflow) > 0;
    }
}