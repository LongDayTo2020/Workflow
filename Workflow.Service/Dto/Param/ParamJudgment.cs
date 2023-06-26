namespace Workflow.Service.Dto.Param;

public class ParamJudgment
{
    public int WorkflowRecordId { get; set; }
    public int WorkflowStepId { get; set; }
    public string Approver { get; set; } = "";
    public string? Description { get; set; }
    public DateTime? ApprovalTime { get; set; }
    public DateTime ExpirationTime { get; set; }
    public string? Status { get; set; }
}