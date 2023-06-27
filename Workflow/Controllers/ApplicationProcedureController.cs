using Microsoft.AspNetCore.Mvc;
using Workflow.Service.Dto.Param;
using Workflow.Service.Interface;

namespace Workflow.Controllers
{
    /// <summary>
    /// 流程紀錄API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationProcedureController : ControllerBase
    {
        private readonly IApplicationProcedureService _applicationProcedureService;

        public ApplicationProcedureController(IApplicationProcedureService applicationProcedureService)
        {
            _applicationProcedureService = applicationProcedureService;
        }

        /// <summary>
        /// 全部流程API
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult GetAll() => Ok(_applicationProcedureService.GetAll());

        /// <summary>
        /// 指定流程
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{workflowRecordId:int}")]
        public IActionResult Get([FromRoute] int workflowRecordId) =>
            Ok(_applicationProcedureService.Get(workflowRecordId));

        /// <summary>
        /// 新增流程
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        public IActionResult Add([FromBody] ParamApplicationProcedure applicationProcedure) =>
            Ok(_applicationProcedureService.Add(applicationProcedure));

        /// <summary>
        /// 同意流程
        /// </summary>
        /// <returns></returns>
        [HttpPut("[action]/{workflowRecordId:int}")]
        public IActionResult Approval([FromRoute] int workflowRecordId, [FromBody] ParamJudgment judgment) =>
            Ok(_applicationProcedureService.Approval(workflowRecordId, judgment));

        /// <summary>
        /// 拒絕流程
        /// </summary>
        /// <returns></returns>
        [HttpPut("[action]/{workflowRecordId:int}")]
        public IActionResult Reject([FromRoute] int workflowRecordId, [FromBody] ParamJudgment judgment) =>
            Ok(_applicationProcedureService.Reject(workflowRecordId, judgment));

        /// <summary>
        /// 新增流程檔案
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        public IActionResult AddFile([FromForm] ParamApplicationProcedureFile applicationProcedureFile) =>
            Ok(_applicationProcedureService.AddFile(applicationProcedureFile));

        /// <summary>
        /// 刪除流程檔案
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]/{applicationProcedureId:int}/{applicationProcedureFileId:int}")]
        public IActionResult DeleteFile([FromRoute] int applicationProcedureId,
            [FromRoute] int applicationProcedureFileId) =>
            Ok(_applicationProcedureService.DeleteFile(applicationProcedureId, applicationProcedureFileId));
    }
}