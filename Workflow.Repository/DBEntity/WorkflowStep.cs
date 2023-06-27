using System.ComponentModel.DataAnnotations.Schema;

namespace Workflow.Repository.DBEntity;

[Dapper.Contrib.Extensions.Table("workflow_steps")]
public class WorkflowStep
{
    public int Id { get; set; }
    [Column("workflow_id")]
    public int WorkflowId { get; set; }
    public int Step { get; set; }
    public int Final { get; set; } = 0;
    public string Name { get; set; } = "";
    public DateTime CreateTime { get; set; }
    public string CreateUser { get; set; } = "";
    public DateTime? UpdateTime { get; set; }
    public string? UpdateUser { get; set; }
}