﻿namespace Workflow.Repository.DBEntity;

public class WorkflowRecordFile
{
    public int Id { get; set; }
    public int WorkflowRecordId { get; set; }
    public int WorkflowStepId { get; set; }
    public string Name { get; set; } = "";
    public string ShowName { get; set; } = "";
    public string Type { get; set; } = "";
    public long Length { get; set; }
    public string? Location { get; set; } = "";
    public string? Description { get; set; }
    public DateTime CreateTime { get; set; }
    public string CreateUser { get; set; } = "";
    public DateTime? UpdateTime { get; set; }
    public string? UpdateUser { get; set; }
}