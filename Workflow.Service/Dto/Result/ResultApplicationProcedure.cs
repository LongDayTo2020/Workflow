namespace Workflow.Service.Dto.Result;

public class ResultApplicationProcedure
{
    public int Id { get; set; }
    public int WorkflowId { get; set; }
    public int Priority { get; set; }
    public TimeSpan ExpirationTime { get; set; }
    public string? Status { get; set; }
    public List<ApprovalInfo> ApprovalInfoList { get; set; } = new List<ApprovalInfo>();
}

public class ApprovalInfo
{
    public int WorkflowRecordId { get; set; }
    public int WorkflowStepId { get; set; }
    public string Approver { get; set; } = "";
    public string? Description { get; set; }
    public DateTime? ApprovalTime { get; set; }
    public DateTime ExpirationTime { get; set; }
    public string? Status { get; set; }
}