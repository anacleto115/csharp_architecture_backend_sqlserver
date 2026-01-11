using lib_domain_context;
using System.Collections.Generic;

namespace lib_application_core.Core
{
    public class FactoryApplication
    {
        public static IFactory<IApplication>? IFactoryApplication;

        public static IApplication? Get(Dictionary<string, object>? data)
        {
            if (IFactoryApplication == null)
                return null;

            return IFactoryApplication.Get(data);
        }
    }
}