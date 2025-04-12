using Grpc.Core;
using System.Text.Json;

namespace SafineBackEnd.Application.Shared
{
    public static class RequestMetaDataExtensions
    {
        public static Dictionary<string, string> GetCliams(this Metadata requestHeader)
        {
            var claims = requestHeader.Get("claims") ?? throw new ArgumentNullException("Claims not found");
            return JsonSerializer.Deserialize<Dictionary<string, string>>(claims.Value);
        }
        public static string GetManagerId(this Metadata requestHeader)
        => GetCliams(requestHeader).FirstOrDefault(a => a.Key == "aid").Value;
    }
}
