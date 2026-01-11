using System.Collections.Generic;

namespace lib_domain_context
{
    public interface IParser<T>
    {
        T? CreateEntity(object[] ItemArray);
        T? ToEntity(Dictionary<string, object>? data);
        Dictionary<string, object>? ToDictionary(T? entity);
        bool Validate(T? entity);
    }
}