namespace Workflow.Repository.DBEntity;

public class WorkflowStep
{
    public int Id { get; set; }
    public int WorkflowId { get; set; }
    public int Step { get; set; }
    public int Final { get; set; } = 0;
    public string Name { get; set; } = "";
    public TimeSpan CreateTime { get; set; }
    public string CreateUser { get; set; } = "";
    public TimeSpan? UpdateTime { get; set; }
    public string? UpdateUser { get; set; }
}