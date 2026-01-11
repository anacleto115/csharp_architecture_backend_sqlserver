namespace lib_domain_context
{
    public interface IService
    {
        IApplication? App { set; }
        object? Select();
        object? Insert();
        object? Update();
        object? Delete();
    }
}