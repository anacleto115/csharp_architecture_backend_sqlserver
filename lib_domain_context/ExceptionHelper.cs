using System;
using System.Collections.Generic;

namespace lib_domain_context
{
    public class ExceptionHelper
    {
        public static Dictionary<string, object>? Convert(
            Exception exception,
            Dictionary<string, object>? response)
        {
            response = response ?? new Dictionary<string, object>();
            try
			{

                var message = exception.ToString();
                if (message.Length >= 50)
                    message = message.Substring(0, 50);

                response["Error"] = message;
                return response;
            }
			catch (Exception ex)
            {
                response["Error"] = ex.ToString();
                return response;
            }
        }
    }
}