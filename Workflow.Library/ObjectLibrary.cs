using System.Reflection;

namespace Workflow.Library;

public static class ObjectLibrary
{
    public static void CloneProperties(object source, object destination)
    {
        var sourceType = source.GetType();
        var destinationType = destination.GetType();

        var sourceProperties = sourceType.GetProperties();
        PropertyInfo?[] destinationProperties = destinationType.GetProperties();

        foreach (var sourceProperty in sourceProperties)
        {
            var destinationProperty = Array.Find(destinationProperties,
                p => p?.Name == sourceProperty.Name && p.PropertyType == sourceProperty.PropertyType);

            if (destinationProperty == null || !destinationProperty.CanWrite) continue;
            var value = sourceProperty.GetValue(source);
            destinationProperty.SetValue(destination, value);
        }
    }
}