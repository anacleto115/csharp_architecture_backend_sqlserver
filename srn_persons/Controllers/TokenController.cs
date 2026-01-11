using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using lib_service_core;
using System;
using System.Collections.Generic;
using System.IO;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using lib_domain_context;
using lib_utilities.Utilities;
using System.Security.Claims;
using System.Text;

namespace srn_persons.Controller
{
    public class TokenController : ControllerBase, IToken
    {
        [HttpPost]
        [AllowAnonymous]
        [Route("Token/Authenticate")]
        public string? Authenticate()
        {
            try
            {
                var income = new StreamReader(Request.Body).ReadToEnd().ToString();
                var data = JsonHelper.ConvertToObject(income!);

                if (!data.ContainsKey("User") ||
                    data["User"].ToString()! != ServiceData.UserData)
                {
                    return JsonHelper.ConvertToString(
                        new Dictionary<string, object>() { { "Error", "lbNoAutenticacion" } });
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, data["User"].ToString()!)
                }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ServiceData.KeyToken!)),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                var response = new Dictionary<string, object>();
                response["Token"] = tokenHandler.WriteToken(token);
                response["Expires"] = tokenDescriptor.Expires;
                return JsonHelper.ConvertToString(response);
            }
            catch (Exception ex)
            {
                var response = new Dictionary<string, object>();
                response = ExceptionHelper.Convert(ex, response);
                return JsonHelper.ConvertToString(response!);
            }
        }

        public bool Validate(Dictionary<string, object>? data)
        {
            try
            {
                if (DataInRequest(data))
                    return true;
                if (!DataInDictionary(data))
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DataInRequest(Dictionary<string, object>? data)
        {
            try
            {
                var req = (Microsoft.AspNetCore.Http.HttpRequest)data!["Req"];
                if (req.Headers == null)
                    return false;

                var authorizationHeader = req.Headers["Authorization"].ToString();
                if (!authorizationHeader.StartsWith("Bearer"))
                    return false;

                authorizationHeader = authorizationHeader.Replace("Bearer ", "");
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken token = tokenHandler.ReadToken(authorizationHeader);
                if (DateTime.UtcNow > token.ValidTo)
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool DataInDictionary(Dictionary<string, object>? data)
        {
            try
            {
                var authorizationHeader = data!["Bearer"].ToString();
                authorizationHeader = authorizationHeader!.Replace("Bearer ", "");
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken token = tokenHandler.ReadToken(authorizationHeader);
                if (DateTime.UtcNow > token.ValidTo)
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}