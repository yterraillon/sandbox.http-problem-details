using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace ConsoleApp;

public class ProblemDetailsHttpMessageHandler : DelegatingHandler
{
    public ProblemDetailsHttpMessageHandler() : base(new HttpClientHandler()) { }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        var mediaType = response.Content.Headers.ContentType?.MediaType;
        if (mediaType != null &&
            mediaType.Equals("application/problem+json", StringComparison.InvariantCultureIgnoreCase))
        {
            // TODO : needs better deserialization
            var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>(new JsonSerializerOptions(), cancellationToken) ?? new ProblemDetails();
            throw new Exception(problemDetails.Detail);
        }

        return response;
    }
}