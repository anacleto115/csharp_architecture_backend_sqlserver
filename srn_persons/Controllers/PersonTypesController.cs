using lib_application_core.Core;
using lib_service_core.Interfaces;
using srn_persons.Core;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using lib_domain_context;

namespace srn_persons.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PersonTypesController : ServiceBase, IPersonTypesService
    {
        public override Dictionary<string, object>? Load(Dictionary<string, object>? data)
        {
            data = base.Load(data);
            data!["Type"] = Types.PersonTypes;
            App = App ?? FactoryApplication.Get(data);
            return data;
        }

        [HttpPost]
        public override object? Select() { return base.Select(); }
    }
}