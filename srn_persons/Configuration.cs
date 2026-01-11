using lib_domain_context;
using lib_utilities.Utilities;
//using Microsoft.Azure.KeyVault;
//using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace srn_persons
{
    public class Configuration : lib_domain_context.IConfiguration
    {
        private static Dictionary<string, string> data;

        public string Get(string key)
        {
            try
            {
                switch (ServiceData.Configuration)
                {
                    //case ServiceData.ServiceConfig.Keyvault: 
                    //    return KeyVaut(key);
                    case ServiceData.ServiceConfig.Service:
                        return Service(key);
                    default: return Local(key);
                }
            }
            catch
            {
                return null;
            }
        }

        private string Service(string key)
        {
            var response = string.Empty;
            if (Startup.Configuration != null)
                response = Startup.Configuration!
                    .GetSection(key!).Value;

            if (Startup.Configuration != null)
                response = Startup.Configuration!
                    .GetSection("Settings")
                    .GetSection(key!).Value;

            if (Startup.Configuration != null &&
                string.IsNullOrEmpty(response))
                response = Startup.Configuration!
                    .GetConnectionString(key!)!;
            return response;
        }

        private string Local(string key)
        {
            if (data == null)
            {
                StreamReader jsonStream = File.OpenText(ServiceData.JsonPath!);
                var json = jsonStream.ReadToEnd();
                data = JsonHelper.ConvertToObject<Dictionary<string, string>>(json)!;
            }
            return data![key!];
        }

        //private string KeyVaut(string key)
        //{
        //    var keyVaultEndpoint = "https://kvnamews.vault.azure.net";
        //    var azureServiceTokenProvider = new AzureServiceTokenProvider();
        //    var keyVaultClient = new KeyVaultClient(
        //        new KeyVaultClient.AuthenticationCallback(
        //            azureServiceTokenProvider.KeyVaultTokenCallback));
        //    return keyVaultClient.GetSecretAsync(keyVaultEndpoint, key).Result.Value;
        //}
    }
}