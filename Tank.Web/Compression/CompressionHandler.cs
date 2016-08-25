using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Tank.Web.Compression
{
    public class CompressionHandler : DelegatingHandler
    {
        public List<ICompressor> Compressors { get; private set; }

        public CompressionHandler()
        {
            Compressors = new List<ICompressor>();

            Compressors.Add(new GZipCompressor());
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (request.Headers.AcceptEncoding != null && request.Headers.AcceptEncoding.Count != 0)
            {
                var encoding = request.Headers.AcceptEncoding.First();

                var compressor = Compressors.FirstOrDefault(c => c.EncodingType.Equals(encoding.Value, StringComparison.InvariantCultureIgnoreCase));

                if (compressor != null)
                {
                    response.Content = new CompressedContent(response.Content, compressor);
                }
            }

            return response;
        }
    }
}