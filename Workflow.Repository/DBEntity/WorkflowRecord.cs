using System.ComponentModel.DataAnnotations.Schema;

namespace Workflow.Repository.DBEntity;

public class WorkflowRecord
{
    public int Id { get; set; }
    [Column("workflow_id")]
    public int WorkflowId { get; set; }
    public int Priority { get; set; }
    public DateTime ExpirationTime { get; set; }
    public string? Status { get; set; }
    public DateTime CreateTime { get; set; }
    public string CreateUser { get; set; } = "";
    public DateTime? UpdateTime { get; set; }
    public string? UpdateUser { get; set; }
}