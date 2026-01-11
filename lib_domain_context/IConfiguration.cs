namespace lib_domain_context
{
    public interface IConfiguration
    {
        string? Get(string? key);
    }
}