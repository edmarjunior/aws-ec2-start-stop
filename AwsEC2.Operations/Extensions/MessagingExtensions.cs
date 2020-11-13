using System.Net;
using System.Runtime.CompilerServices;

namespace AwsEC2.Operations.Extensions
{
    public static class MessagingExtensions
    {
        public static string GetStatusMessageFromHttpStatusCode(this HttpStatusCode statusCode, [CallerMemberName] string methodName = "") =>
            statusCode == HttpStatusCode.OK
                ? $"The { methodName } method returned 'OK' - the operation completed successfully."
                : $"The { methodName } method returned an HTTP Status Code of { (int)statusCode } ({ statusCode }). Please check that the operation completed as expected.";
    }
}
