using System.Data;
using Dapper;
using Workflow.Repository.DBEntity;
using Workflow.Repository.Interface;

namespace Workflow.Repository.Implement;

public class WorkflowApprovalRepository : IWorkflowApprovalRepository
{
    private readonly IDbConnection _dbConnection;

    public WorkflowApprovalRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }


    public IEnumerable<WorkflowApproval> Query()
    {
        string sql = @"
select *
from workflow_approvals;
";
        return _dbConnection.Query<WorkflowApproval>(sql);
    }

    public bool Create(WorkflowApproval workflow)
    {
        string sql = @"
insert into workflow_approvals (workflow_record_id, workflow_step_id, approver, description, approval_time,
                                expiration_time, status, create_time, create_user)
values (
 @WorkflowRecordId 
,@WorkflowStepId
,@Approver 
,@Description  
,@ApprovalTime 
,@ExpirationTime
,@Status 
,@CreateTime  
,@CreateUser );
";
        return _dbConnection.Execute(sql, workflow) > 0;
    }

    public bool Update(WorkflowApproval workflow)
    {
        string sql = @"
update workflow_approvals
set  
approver = @Approver ,
description = @Description ,
approval_time = @ApprovalTime ,
expiration_time = @ExpirationTime ,
status = @Status ,
update_time = @UpdateTime ,
update_user = @UpdateUser 
where workflow_record_id = @WorkflowRecordId AND workflow_step_id = @WorkflowStepId ;
";
        return _dbConnection.Execute(sql, workflow) > 0;
    }

    public bool Delete(WorkflowApproval workflow)
    {
        string sql = @"
delete
from workflow_approvals
where workflow_record_id = @WorkflowRecordId AND workflow_step_id = @WorkflowStepId ;
";
        return _dbConnection.Execute(sql, workflow) > 0;
    }
}