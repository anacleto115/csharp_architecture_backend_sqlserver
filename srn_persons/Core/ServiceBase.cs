using lib_application_context.Core;
using lib_application_core.Core;
using lib_domain_context;
using lib_service_core;
using lib_utilities.Utilities;
using Microsoft.AspNetCore.Mvc;
using srn_persons.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace srn_persons.Core
{
    public abstract class ServiceBase : ControllerBase, IService
    {
        public IApplication? App { get; set; }
        protected IToken? IToken;
        protected IConfiguration? IConfiguration;

        public virtual Dictionary<string, object>? GetValue()
        {
            return JsonHelper.ConvertToObject(
                new StreamReader(Request.Body).ReadToEnd().ToString());
        }

        public virtual Dictionary<string, object>? Load(Dictionary<string, object>? data)
        {
            data = data ?? new Dictionary<string, object>();
            IConfiguration = IConfiguration ?? new Configuration();
            FactoryApplication.IFactoryApplication =
                FactoryApplication.IFactoryApplication ?? new FactoryApplicationContext();
            IToken = IToken ?? new TokenController();

            data["Architecture"] = Architecture.Services;
            data["stringConnection"] = IConfiguration!.Get("Default")!;
            if (data.ContainsKey("IApplication"))
                App = (IApplication)data["IApplication"];
            return data;
        }

        public virtual object? Select()
        {
            var response = new Dictionary<string, object>();
            try
            {
                var data = FuncValidate();
                if (data!.ContainsKey("Error"))
                {
                    return StatusCode(400,
                        JsonHelper.ConvertToString(data!));
                }
                response = App!.Select(data);
                if (response!.ContainsKey("Error"))
                {
                    return StatusCode(400,
                        JsonHelper.ConvertToString(response!));
                }
                return StatusCode(200,
                    JsonHelper.ConvertToString(response!));
            }
            catch (Exception ex)
            {
                response = ExceptionHelper.Convert(ex, response);
                return StatusCode(500,
                    JsonHelper.ConvertToString(response!));
            }
        }

        public virtual object? Insert()
        {
            var response = new Dictionary<string, object>();
            try
            {
                var data = FuncValidate();
                if (data!.ContainsKey("Error"))
                {
                    return StatusCode(400,
                        JsonHelper.ConvertToString(data!));
                }
                response = App!.Insert(data);
                if (response!.ContainsKey("Error"))
                {
                    return StatusCode(400,
                        JsonHelper.ConvertToString(response!));
                }
                return StatusCode(200,
                    JsonHelper.ConvertToString(response!));
            }
            catch (Exception ex)
            {
                response = ExceptionHelper.Convert(ex, response);
                return StatusCode(500,
                    JsonHelper.ConvertToString(response!));
            }
        }

        public virtual object? Update()
        {
            var response = new Dictionary<string, object>();
            try
            {
                var data = FuncValidate();
                if (data!.ContainsKey("Error"))
                {
                    return StatusCode(400,
                        JsonHelper.ConvertToString(data!));
                }
                response = App!.Update(data);
                if (response!.ContainsKey("Error"))
                {
                    return StatusCode(400,
                        JsonHelper.ConvertToString(response!));
                }
                return StatusCode(200,
                    JsonHelper.ConvertToString(response!));
            }
            catch (Exception ex)
            {
                response = ExceptionHelper.Convert(ex, response);
                return StatusCode(500,
                    JsonHelper.ConvertToString(response!));
            }
        }

        public virtual object? Delete()
        {
            var response = new Dictionary<string, object>();
            try
            {
                var data = FuncValidate();
                if (data!.ContainsKey("Error"))
                {
                    return StatusCode(400,
                        JsonHelper.ConvertToString(data!));
                }
                response = App!.Delete(data);
                if (response!.ContainsKey("Error"))
                {
                    return StatusCode(400,
                        JsonHelper.ConvertToString(response!));
                }
                return StatusCode(200,
                    JsonHelper.ConvertToString(response!));
            }
            catch (Exception ex)
            {
                response = ExceptionHelper.Convert(ex, response);
                return StatusCode(500,
                    JsonHelper.ConvertToString(response!));
            }
        }

        protected virtual Dictionary<string, object> FuncValidate()
        {
            var response = new Dictionary<string, object>();
            try
            {
                var data = GetValue();
                data!["Req"] = Request;
                data = Load(data);
                if (!IToken!.Validate(data))
                {
                    response = new Dictionary<string, object>();
                    response["Error"] = "NoAuthenticate";
                    return response;
                }
                data!.Remove("Req");
                return data;
            }
            catch (Exception ex)
            {
                response["Error"] = ex.ToString();
                return response;
            }
        }
    }
}