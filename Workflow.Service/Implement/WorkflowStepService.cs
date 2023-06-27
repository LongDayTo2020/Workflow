using Workflow.Library;
using Workflow.Repository.DBEntity;
using Workflow.Repository.Interface;
using Workflow.Service.Dto.Param;
using Workflow.Service.Dto.Result;
using Workflow.Service.Interface;

namespace Workflow.Service.Implement;

public class WorkflowStepService : IWorkflowStepService
{
    private readonly IWorkflowStepRepository _workflowStepRepository;

    public WorkflowStepService(IWorkflowStepRepository workflowStepRepository)
    {
        _workflowStepRepository = workflowStepRepository;
    }

    public List<ResultWorkflowStep> Get(int workflowId)
    {
        var a = _workflowStepRepository.Query()
            .Where(x => x.WorkflowId == workflowId);
        var s = a
            .Select(q =>
            {
                var resultWorkflowStep = new ResultWorkflowStep();
                ObjectLibrary.CloneProperties(q, resultWorkflowStep);
                return resultWorkflowStep;
            }).ToList();
        return s;
    }

    public bool Add(ParamWorkflowStep workflowStep)
    {
        var addWorkflowStep = new WorkflowStep();
        ObjectLibrary.CloneProperties(workflowStep, addWorkflowStep);
        return _workflowStepRepository.Create(addWorkflowStep);
    }

    public bool Update(int workflowStepId, ParamWorkflowStep workflowStep)
    {
        var updateWorkflowStep = _workflowStepRepository.Query().FirstOrDefault(x => x.Id == workflowStepId);
        if (updateWorkflowStep == null) return false;
        ObjectLibrary.CloneProperties(workflowStep, updateWorkflowStep);
        return _workflowStepRepository.Update(updateWorkflowStep);
    }

    public bool Delete(int workflowStepId)
    {
        var deleteWorkflowStep = _workflowStepRepository.Query().FirstOrDefault(x => x.Id == workflowStepId);
        if (deleteWorkflowStep == null) return false;
        return _workflowStepRepository.Delete(deleteWorkflowStep);
    }
}