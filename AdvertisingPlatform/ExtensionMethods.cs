using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace AdvertisingPlatform
{
    public static class ExtensionMethods
    {
        public static LocationNode LocalsRootNode = [];

        public static async Task<string> GetRawBodyAsync(this HttpRequest request)
        {
            if (!request.Body.CanSeek)
            {
                // We only do this if the stream isn't *already* seekable,
                // as EnableBuffering will create a new stream instance
                // each time it's called
                request.EnableBuffering();
            }

            request.Body.Position = 0;
            
            var body = await new StreamReader(request.Body, Encoding.UTF8).ReadToEndAsync().ConfigureAwait(false);

            request.Body.Position = 0;

            return body;
        }

    }
}
