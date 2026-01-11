using lib_data_context.Core;
using lib_data_core.Core;
using lib_domain_context;
using System;
using System.Collections.Generic;

namespace lib_application_context.Core
{
    public abstract class App<T> : IApplication
    {
        public IParser<T>? IParser;
        public IRepository? IRepository { get; set; }

        public virtual Dictionary<string, object>? Load(Dictionary<string, object>? data)
        {
            data = data ?? new Dictionary<string, object>();
            FactoryRepository.IFactoryRepository =
                FactoryRepository.IFactoryRepository ?? new FactoryRepositoryContext();
            if (data.ContainsKey("IRepository"))
                IRepository = (IRepository)data["IRepository"];
            return data;
        }

        public virtual Dictionary<string, object>? Select(Dictionary<string, object>? data)
        {
            Dictionary<string, object>? response = null;
            try
            {
                data = FuncValidate(data);
                response = IRepository!.Select(data);
                response = response ?? new Dictionary<string, object>();
                if (IParser != null &&
                    response.ContainsKey("Entities"))
                {
                    var list = (List<T>)response["Entities"];
                    var count = 0;
                    var dic = new Dictionary<string, object>();
                    foreach (var item in list)
                    {
                        dic[count.ToString()] = IParser.ToDictionary(item)!;
                        count++;
                    }
                    response["Entities"] = dic;
                }
                return response;
            }
            catch (Exception ex)
            {
                response = ExceptionHelper.Convert(ex, response);
                return response;
            }
        }

        public virtual Dictionary<string, object>? Insert(Dictionary<string, object>? data)
        {
            Dictionary<string, object>? response = null;
            try
            {
                data = FuncValidate(data);
                data = data ?? new Dictionary<string, object>();
                if (IParser != null &&
                    data.ContainsKey("Entity"))
                    data["Entity"] = IParser.ToEntity((Dictionary<string, object>)data["Entity"])!;
                if (IParser != null &&
                    data.ContainsKey("Entity") &&
                    !IParser.Validate((T)data["Entity"]))
                    return new Dictionary<string, object>() { { "Error", "lbMissingInfo" } };
                response = IRepository!.Insert(data);
                response = response ?? new Dictionary<string, object>();
                if (IParser != null &&
                    response.ContainsKey("Entity"))
                    response["Entity"] = IParser.ToDictionary((T)response["Entity"])!;
                return response;
            }
            catch (Exception ex)
            {
                response = ExceptionHelper.Convert(ex, response);
                return response;
            }
        }

        public virtual Dictionary<string, object>? Update(Dictionary<string, object>? data)
        {
            Dictionary<string, object>? response = null;
            try
            {
                data = FuncValidate(data);
                data = data ?? new Dictionary<string, object>();
                if (IParser != null &&
                    data.ContainsKey("Entity"))
                    data["Entity"] = IParser.ToEntity((Dictionary<string, object>)data["Entity"])!;
                if (IParser != null &&
                    data.ContainsKey("Entity") &&
                    !IParser.Validate((T)data["Entity"]))
                    return new Dictionary<string, object>() { { "Error", "lbMissingInfo" } };
                response = IRepository!.Update(data);
                response = response ?? new Dictionary<string, object>();
                if (IParser != null &&
                    response.ContainsKey("Entity"))
                    response["Entity"] = IParser.ToDictionary((T)response["Entity"])!;
                return response;
            }
            catch (Exception ex)
            {
                response = ExceptionHelper.Convert(ex, response);
                return response;
            }
        }

        public virtual Dictionary<string, object>? Delete(Dictionary<string, object>? data)
        {
            Dictionary<string, object>? response = null;
            try
            {
                data = FuncValidate(data);
                data = data ?? new Dictionary<string, object>();
                if (IParser != null &&
                    data.ContainsKey("Entity"))
                    data["Entity"] = IParser.ToEntity((Dictionary<string, object>)data["Entity"])!;
                if (IParser != null &&
                    data.ContainsKey("Entity") &&
                    !IParser.Validate((T)data["Entity"]))
                    return new Dictionary<string, object>() { { "Error", "lbMissingInfo" } };
                response = IRepository!.Delete(data);
                response = response ?? new Dictionary<string, object>();
                if (IParser != null &&
                    response.ContainsKey("Entity"))
                    response["Entity"] = IParser.ToDictionary((T)response["Entity"])!;
                return response;
            }
            catch (Exception ex)
            {
                response = ExceptionHelper.Convert(ex, response);
                return response;
            }
        }

        protected virtual Dictionary<string, object>? FuncValidate(Dictionary<string, object>? data)
        {
            Dictionary<string, object>? response = null;
            try
            {
                response = Load(data);
                return response;
            }
            catch (Exception ex)
            {
                response = ExceptionHelper.Convert(ex, response);
                return response;
            }
        }
    }
}