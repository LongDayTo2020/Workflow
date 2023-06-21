namespace Workflow.Repository.DBEntity;

public class WorkflowRecordFile
{
    public int Id { get; set; }
    public int WorkflowRecordId { get; set; }
    public int WorkflowStepId { get; set; }
    public string Name { get; set; } = "";
    public string ShowName { get; set; } = "";
    public string Type { get; set; } = "";
    public int Length { get; set; }
    public string? Location { get; set; } = "";
    public string? Description { get; set; }
    public TimeSpan CreateTime { get; set; }
    public string CreateUser { get; set; } = "";
    public TimeSpan? UpdateTime { get; set; }
    public string? UpdateUser { get; set; }
}