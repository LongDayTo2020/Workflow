namespace Workflow.Service.Dto.Param;

public class ParamJudgment
{
    public int WorkflowRecordId { get; set; }
    public int WorkflowStepId { get; set; }
    public string NextApprover { get; set; } = "";
    public string? Description { get; set; }
    public DateTime NextExpirationTime { get; set; }
}