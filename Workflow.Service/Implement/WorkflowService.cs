using Workflow.Library;
using Workflow.Repository.Interface;
using Workflow.Service.Dto.Param;
using Workflow.Service.Dto.Result;
using Workflow.Service.Interface;

namespace Workflow.Service.Implement;

public class WorkflowService : IWorkflowService
{
    private readonly IWorkflowRepository _workflowRepository;

    public WorkflowService(IWorkflowRepository workflowRepository)
    {
        _workflowRepository = workflowRepository;
    }

    public List<ResultWorkflow> GetAll() =>
        _workflowRepository.Query().Select(q =>
        {
            var result = new ResultWorkflow();
            ObjectLibrary.CloneProperties(q, result);
            return result;
        }).ToList();

    public ResultWorkflow? Get(int workflowId) =>
        _workflowRepository.Query()
            .Where(x => x.Id == workflowId)
            .Select(q =>
            {
                var result = new ResultWorkflow();
                ObjectLibrary.CloneProperties(q, result);
                return result;
            }).FirstOrDefault();

    public bool Add(ParamWorkflow workflow)
    {
        var newWorkflow = new Repository.DBEntity.Workflow();
        ObjectLibrary.CloneProperties(workflow, newWorkflow);
        return _workflowRepository.Create(newWorkflow);
    }

    public bool Update(int workflowId, ParamWorkflow workflow)
    {
        var updateWorkflow = _workflowRepository.Query().FirstOrDefault(x => x.Id == workflowId);
        if (updateWorkflow == null) return false;
        ObjectLibrary.CloneProperties(workflow, updateWorkflow);
        return _workflowRepository.Update(updateWorkflow);
    }

    public bool Delete(int workflowId)
    {
        var updateWorkflow = _workflowRepository.Query().FirstOrDefault(x => x.Id == workflowId);
        if (updateWorkflow == null) return false;
        return _workflowRepository.Delete(updateWorkflow);
    }
}