using System.Collections.Generic;

namespace lib_service_core
{
    public interface IToken
    {
        bool Validate(Dictionary<string, object>? data);
        string? Authenticate();
    }
}