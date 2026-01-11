using System.Collections.Generic;

namespace lib_domain_context
{
    public interface IApplication
    {
        IRepository? IRepository { get; set; }
        Dictionary<string, object>? Select(Dictionary<string, object>? data);
        Dictionary<string, object>? Insert(Dictionary<string, object>? data);
        Dictionary<string, object>? Update(Dictionary<string, object>? data);
        Dictionary<string, object>? Delete(Dictionary<string, object>? data);
    }
}