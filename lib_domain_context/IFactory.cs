using System.Collections.Generic;

namespace lib_domain_context
{
    public interface IFactory<T>
    {
        T? Get(Dictionary<string, object>? data);
    }
}