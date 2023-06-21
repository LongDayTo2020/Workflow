namespace Workflow.Repository.DBEntity;

public class WorkflowRecord
{
    public int Id { get; set; }
    public int WorkflowId { get; set; }
    public int Priority { get; set; }
    public TimeSpan ExpirationTime { get; set; }
    public string? Status { get; set; }
    public TimeSpan CreateTime { get; set; }
    public string CreateUser { get; set; } = "";
    public TimeSpan? UpdateTime { get; set; }
    public string? UpdateUser { get; set; }
}