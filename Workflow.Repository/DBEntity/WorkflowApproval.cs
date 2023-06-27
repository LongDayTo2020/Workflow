using System.ComponentModel.DataAnnotations.Schema;

namespace Workflow.Repository.DBEntity;

public class WorkflowApproval
{
    [Column("workflow_record_id")]
    public int WorkflowRecordId { get; set; }
    [Column("workflow_step_id")]
    public int WorkflowStepId { get; set; }
    public string Approver { get; set; } = "";
    public string? Description { get; set; }
    public DateTime? ApprovalTime { get; set; }
    public DateTime ExpirationTime { get; set; }
    public string? Status { get; set; }
    public DateTime CreateTime { get; set; }
    public string CreateUser { get; set; } = "";
    public DateTime? UpdateTime { get; set; }
    public string? UpdateUser { get; set; }
}