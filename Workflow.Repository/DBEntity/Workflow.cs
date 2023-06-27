namespace Workflow.Repository.DBEntity;

public class Workflow
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public DateTime CreateTime { get; set; }
    public string CreateUser { get; set; } = "";
    public DateTime? UpdateTime { get; set; }
    public string? UpdateUser { get; set; }
}