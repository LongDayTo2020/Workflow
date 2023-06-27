namespace Workflow.Service.Dto.Param;

public class ParamWorkflowStep
{
    public int WorkflowId { get; set; }
    public int Step { get; set; }
    public int Final { get; set; } = 0;
    public string Name { get; set; } = "";
}