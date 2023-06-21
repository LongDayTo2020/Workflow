using Microsoft.AspNetCore.Mvc;
using Workflow.Service.Dto.Param;
using Workflow.Service.Interface;

namespace Workflow.Controllers
{
    /// <summary>
    /// 流程步驟維護
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowStepController : ControllerBase
    {
        private readonly IWorkflowStepService _workflowStepService;

        public WorkflowStepController(IWorkflowStepService workflowStepService)
        {
            _workflowStepService = workflowStepService;
        }

        [HttpGet("[action]/{workflowStepId:int}")]
        public IActionResult Get([FromRoute] int workflowStepId) =>
            Ok(_workflowStepService.Get(workflowStepId));

        [HttpPost("[action]")]
        public IActionResult Add([FromBody] ParamWorkflowStep workflowStep) =>
            Ok(_workflowStepService.Add(workflowStep));

        [HttpPut("[action]/{workflowStepId:int}")]
        public IActionResult Update([FromRoute] int workflowStepId, ParamWorkflowStep workflowStep) =>
            Ok(_workflowStepService.Update(workflowStepId, workflowStep));

        [HttpDelete("[action]/{workflowStepId:int}")]
        public IActionResult Delete([FromRoute] int workflowStepId) =>
            Ok(_workflowStepService.Delete(workflowStepId));
    }
}