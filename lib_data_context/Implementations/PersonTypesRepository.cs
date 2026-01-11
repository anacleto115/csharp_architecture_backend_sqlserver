using lib_data_context.Core;
using lib_domain_entities.Models;
using lib_infrastructure.Implementations;
using System;
using System.Collections.Generic;
using lib_data_core.Interfaces;
using lib_data_core.Core;
using lib_domain_context;

namespace lib_data_context.Implementations
{
    public class PersonTypesRepository : Repository<PersonTypes>, IPersonTypesRepository
    {
        public PersonTypesRepository(Dictionary<string, object> data) { parser = new PersonTypesParser(); }

        public Dictionary<string, object>? Select(Dictionary<string, object>? data)
        {
            Dictionary<string, object>? response = null;
            try
            {
                data = data ?? new Dictionary<string, object>();
                var parameters = new List<Parameters>();

                data!["Parameters"] = parameters;
                data["Procedure"] = "sp_select_per_types";
                response = Execute(data);
                if (response == null)
                    return new Dictionary<string, object>() { { "Error", "lbNoAnswerDB" } };
                if (response.ContainsKey("Error"))
                    return response;
                response["Response"] = "OK";
                return response;
            }
            catch (Exception ex)
            {
                response = ExceptionHelper.Convert(ex, response);
                return response;
            }
        }

        public Dictionary<string, object>? Insert(Dictionary<string, object>? data)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object>? Update(Dictionary<string, object>? data)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object>? Delete(Dictionary<string, object>? data)
        {
            throw new NotImplementedException();
        }
    }
}