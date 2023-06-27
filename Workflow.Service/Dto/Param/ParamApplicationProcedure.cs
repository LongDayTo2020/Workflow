namespace Workflow.Service.Dto.Param;

public class ParamApplicationProcedure
{
    public int WorkflowId { get; set; }
    public int Priority { get; set; }
    public DateTime ExpirationTime { get; set; }
    public string Approver { get; set; } = "";
    public string? Description { get; set; }
}