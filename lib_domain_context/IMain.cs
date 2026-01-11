using System.Collections.Generic;
using System.Threading.Tasks;

namespace lib_domain_context
{
    public interface IMain
    {
        Task<Dictionary<string, object>?> GetFile(Dictionary<string, object>? data);
    }
}