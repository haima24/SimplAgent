namespace SimplAgent.Shared.Dtos.Services;

public static class MappingExtensions
{
    public static TDestination MapTo<TDestination>(this object source)
        where TDestination : new()
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        TDestination destination = new();

        var sourceProps = source.GetType().GetProperties();
        var destProps = typeof(TDestination).GetProperties();

        foreach (var destProp in destProps)
        {
            var sourceProp = sourceProps.FirstOrDefault(p => p.Name == destProp.Name
                                                             && p.PropertyType == destProp.PropertyType);

            if (sourceProp != null && destProp.CanWrite)
            {
                destProp.SetValue(destination, sourceProp.GetValue(source));
            }
        }

        return destination;
    }
}
