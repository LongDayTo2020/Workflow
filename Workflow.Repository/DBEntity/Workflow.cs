namespace Workflow.Repository.DBEntity;

public class Workflow
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public TimeSpan CreateTime { get; set; }
    public string CreateUser { get; set; } = "";
    public TimeSpan? UpdateTime { get; set; }
    public string? UpdateUser { get; set; }
}