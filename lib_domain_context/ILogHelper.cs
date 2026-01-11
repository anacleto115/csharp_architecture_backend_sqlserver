using System;
using System.Runtime.CompilerServices;

namespace lib_domain_context
{
    public class LogHelper
    {
        public static ILogHelper? ILogHelper;

        public static void Log(Exception exception, bool subError = false, [CallerMemberName] string? caller = "", [CallerFilePath] string? file = "")
        {
            if (ILogHelper == null)
                return;

            ILogHelper.Log(exception, subError, caller, file);
        }
    }

    public interface ILogHelper
    {
        void Log(Exception exception, bool subError = false, [CallerMemberName] string? caller = "", [CallerFilePath] string? file = "");
    }
}