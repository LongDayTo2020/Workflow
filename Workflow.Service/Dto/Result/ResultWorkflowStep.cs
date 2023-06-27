namespace Workflow.Service.Dto.Result;

public class ResultWorkflowStep
{
    public int Id { get; set; }
    public int WorkflowId { get; set; }
    public int Step { get; set; }
    public int Final { get; set; } = 0;
    public string Name { get; set; } = "";
}