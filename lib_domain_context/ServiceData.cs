namespace lib_domain_context
{
    public class ServiceData
    {
        public enum ServiceConfig { Service, Keyvault, Local }

        public static string? JsonPath = @"E:\Configuration\persons.config.json";
        public static ServiceConfig? Configuration = ServiceConfig.Local;
        public static string? KeyToken = "fetgi25793641vghgHKJGdtsifytsidi3fr6772jhgUTytutyiiyi";
        public static string? UserData = "PUS3r.Pws7JHGgh4h1";
    }
}