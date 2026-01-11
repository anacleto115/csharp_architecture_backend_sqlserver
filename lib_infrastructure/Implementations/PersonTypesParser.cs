using lib_domain_entities.Models;
using lib_domain_context;
using System;
using System.Collections.Generic;

namespace lib_infrastructure.Implementations
{
    public class PersonTypesParser : IParser<PersonTypes>
    {
        public PersonTypes? CreateEntity(object[] ItemArray)
        {
            return new PersonTypes()
            {
                #region Convert DataRow To Entity 
                Id = Convert.ToInt32(ItemArray[0].ToString()),
                Name = ItemArray[1] == DBNull.Value ? null : ItemArray[1].ToString()
                #endregion
            };
        }

        public PersonTypes? ToEntity(Dictionary<string, object>? data)
        {
            var entity = new PersonTypes();
            if (data == null)
                return entity;

            entity.Id = Convert.ToInt32(data["Id"]);
            if (data.ContainsKey("Name"))
                entity.Name = data["Name"].ToString();
            return entity;
        }

        public Dictionary<string, object>? ToDictionary(PersonTypes? entity)
        {
            var data = new Dictionary<string, object>();
            if (entity == null)
                return data;

            data["Id"] = entity.Id.ToString();
            if (!string.IsNullOrEmpty(entity.Name))
                data["Name"] = entity.Name.ToString();
            return data;
        }

        public bool Validate(PersonTypes? entity)
        {
            if (entity == null)
                return false;

            if (string.IsNullOrEmpty(entity.Name))
                return false;
            return true;
        }
    }
}