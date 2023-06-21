using Microsoft.AspNetCore.Mvc;
using Workflow.Service.Dto.Param;
using Workflow.Service.Interface;

namespace Workflow.Controllers
{
    /// <summary>
    /// 流程維護
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowController : ControllerBase
    {
        private readonly IWorkflowService _workflowService;

        public WorkflowController(IWorkflowService workflowService)
        {
            _workflowService = workflowService;
        }

        [HttpGet("[action]")]
        public IActionResult GetAll() => Ok(_workflowService.GetAll());

        [HttpGet("[action]/{workflowId:int}")]
        public IActionResult Get([FromRoute] int workflowId) => Ok(_workflowService.Get(workflowId));

        [HttpPost("[action]")]
        public IActionResult Add([FromBody] ParamWorkflow workflow) => Ok(_workflowService.Add(workflow));

        [HttpPut("[action]/{workflowId:int}")]
        public IActionResult Update([FromRoute] int workflowId, ParamWorkflow workflow) =>
            Ok(_workflowService.Update(workflowId, workflow));

        [HttpDelete("[action]/{workflowId:int}")]
        public IActionResult Delete([FromRoute] int workflowId) => Ok(_workflowService.Delete(workflowId));
    }
}