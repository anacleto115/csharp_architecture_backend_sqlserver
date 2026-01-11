using lib_domain_context;
using lib_data_context.Implementations;
using System.Collections.Generic;

namespace lib_data_context.Core
{
    public class FactoryRepositoryContext : IFactory<IRepository>
    {
        public IRepository? Get(Dictionary<string, object>? data)
        {
            data = data ?? new Dictionary<string, object>();
            Types type = (Types)data["Type"];
            switch (type)
            {
                case Types.PersonTypes: return new PersonTypesRepository(data);
                default: return null;
            }
        }
    }
}