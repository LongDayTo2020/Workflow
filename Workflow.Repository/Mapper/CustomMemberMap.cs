using System.Reflection;
using Dapper;

namespace Workflow.Repository.Mapper;

public class CustomMemberMap : SqlMapper.IMemberMap
{
    public CustomMemberMap(PropertyInfo property, string columnName, FieldInfo? field = null,
        ParameterInfo? parameter = null)
    {
        Property = property;
        ColumnName = columnName;
        Field = field;
        Parameter = parameter;
    }

    public PropertyInfo Property { get; }
    public string ColumnName { get; }
    public Type MemberType => Property.PropertyType;
    public FieldInfo? Field { get; }
    public ParameterInfo? Parameter { get; }

    public string Name => Property.Name;

    public bool CanWrite => Property.CanWrite;


    public void SetValue(object obj, object value)
    {
        Property.SetValue(obj, value);
    }

    public object? GetValue(object obj)
    {
        return Property.GetValue(obj);
    }
}