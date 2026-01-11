using System.Collections.Generic;
using System.Threading.Tasks;

namespace lib_domain_context
{
    public enum Loading
    {
        ADD,
        REMOVE
    }

    public enum Action
    {
        OPEN,
        CLOSE
    }

    public interface IScreen
    {
        Task Loading(Loading? action);
        void MoveFocus();
        Task Change(Dictionary<string, object>? data);
    }
}
