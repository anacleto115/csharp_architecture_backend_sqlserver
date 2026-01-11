using lib_application_context.Core;
using lib_application_core.Interfaces;
using lib_data_core.Core;
using lib_domain_context;
using lib_domain_entities.Models;
using lib_infrastructure.Implementations;
using System.Collections.Generic;

namespace lib_application_context.Implementations
{
    internal class PersonTypesApp : App<PersonTypes>, IPersonTypesApp
    {
        public PersonTypesApp(Dictionary<string, object> data) { }

        public override Dictionary<string, object>? Load(Dictionary<string, object>? data)
        {
            data = base.Load(data);
            data = data ?? new Dictionary<string, object>();
            if (data.ContainsKey("Architecture") &&
                (Architecture)data["Architecture"] == Architecture.Services)
                IParser = new PersonTypesParser();
            if (!data.ContainsKey("IRepository"))
                IRepository = IRepository ?? FactoryRepository.Get(data);
            return data;
        }
    }
}