using System;
using System.Text;

namespace AwsEC2.Operations.Extensions
{
    public static class ExceptionExtensions
    {
        public static string ToFriendlyExceptionString(this Exception exception, bool includeStack = true)
        {
            StringBuilder exceptionStringBuilder = new StringBuilder();

            if (exception != null)
            {
                // A valid exception is in scope - append messages from this exception and any inner exception (if present)
                exceptionStringBuilder.AppendLine($"The following exception has occurred: { exception.Message }");
                exceptionStringBuilder.AppendLine(exception.InnerException != null
                    ? $"An inner exception was detected as follows: { exception.InnerException.Message }" : "No inner exception was detected.");

                // Include stack information as specified by the caller
                if (includeStack && !string.IsNullOrWhiteSpace(exception.StackTrace))
                {
                    exceptionStringBuilder.AppendLine($"Stack trace: { exception.StackTrace }");
                }
            }

            return exceptionStringBuilder.ToString();
        }
    }
}
