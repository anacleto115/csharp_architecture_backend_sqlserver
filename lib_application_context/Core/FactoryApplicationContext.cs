using lib_application_context.Implementations;
using lib_domain_context;
using System.Collections.Generic;

namespace lib_application_context.Core
{
    public class FactoryApplicationContext : IFactory<IApplication>
    {
        public IApplication? Get(Dictionary<string, object>? data)
        {
            data = data ?? new Dictionary<string, object>();
            Types type = (Types)data["Type"];
            switch (type)
            {
                case Types.PersonTypes: return new PersonTypesApp(data);
                default: return null;
            }
        }
    }
}