using lib_domain_context;
using System.Collections.Generic;

namespace lib_data_core.Core
{
    public class FactoryRepository
    {
        public static IFactory<IRepository>? IFactoryRepository;

        public static IRepository? Get(Dictionary<string, object>? data)
        {
            if (IFactoryRepository == null)
                return null;

            return IFactoryRepository.Get(data);
        }
    }
}