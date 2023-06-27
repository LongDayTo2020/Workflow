using Microsoft.AspNetCore.Http;

namespace Workflow.Service.Dto.Param;

public class ParamApplicationProcedureFile
{
    public int WorkflowRecordId { get; set; }
    public int WorkflowStepId { get; set; }
    public IFormFile File { get; set; }
    public string? Description { get; set; }
}