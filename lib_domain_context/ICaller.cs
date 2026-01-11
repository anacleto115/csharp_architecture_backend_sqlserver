using System.Collections.Generic;
using System.Threading.Tasks;

namespace lib_domain_context
{
    public interface ICaller
    {
        Task<Dictionary<string, object>?> Execute(Dictionary<string, object>? data);
    }

    public class FactoryCaller
    {
        public static IFactory<ICaller>? IFactoryCaller;

        public static ICaller? Get(Dictionary<string, object>? data)
        {
            if (IFactoryCaller == null)
                return null;

            return IFactoryCaller.Get(data);
        }
    }
}