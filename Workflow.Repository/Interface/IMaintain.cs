using System.Dynamic;

namespace Workflow.Repository.Interface;

public interface IMaintain<T>
{
    IEnumerable<T> Query();

    bool Create(T param);

    bool Update(T param);

    bool Delete(params T[] paramList);
}