using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Dapper;

namespace Workflow.Repository.Mapper;

public class CustomMapper<T> : SqlMapper.ITypeMap
{
    private readonly PropertyInfo[] _properties;
    private readonly string[] _columnNames;

    public CustomMapper()
    {
        _properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        _columnNames = _properties.Select(GetColumnName).ToArray();
    }

    public ConstructorInfo? FindConstructor(string[] names, Type[] types)
    {
        return typeof(T).GetConstructor(Type.EmptyTypes);
    }

    public ConstructorInfo? FindExplicitConstructor()
    {
        return null;
    }

    public SqlMapper.IMemberMap? GetConstructorParameter(ConstructorInfo constructor, string columnName)
    {
        return null;
    }

    public SqlMapper.IMemberMap? GetMember(string columnName)
    {
        var columnIndex = Array.IndexOf(_columnNames, columnName);
        if (columnIndex < 0) return null;
        var property = _properties[columnIndex];
        return new CustomMemberMap(property, columnName);

    }

    private static string GetColumnName(PropertyInfo property)
    {
        var attribute = property.GetCustomAttribute<ColumnAttribute>(inherit: true);
        if (attribute != null && !string.IsNullOrEmpty(attribute.Name))
        {
            return attribute.Name;
        }

        return ToDbColumnName(property.Name);
    }

    private static string ToDbColumnName(string propertyName)
    {
        return string
            .Concat(propertyName.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()))
            .ToLower();
    }
}