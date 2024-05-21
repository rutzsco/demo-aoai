using Azure.Core;
using Demo.AI.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;
using System.Reflection.Metadata;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Demo.AI.API
{
    internal static class WebApplicationExtensions
    {

        internal static WebApplication MapApi(this WebApplication app)
        {
            var api = app.MapGroup("api");


            api.MapPost("summarize", OnProcessSummarizeAsync);
            return app;
        }

        private static async Task<IResult> OnProcessSummarizeAsync(SummarizeRequest request, [FromServices] Kernel kernel)
        {
            var summarizeFunction = kernel.Plugins.GetFunction(DefaultSettings.SummarizeDocumentPluginName, DefaultSettings.SummarizeDocumentFunctionName);

            var arguments = new KernelArguments { { "document", request.DocumentText } };
            var result = await kernel.InvokeAsync(summarizeFunction, arguments);
            var response = new
            {
                Summary = result.GetValue<string>()
            };
            return TypedResults.Ok(response);
        }
    }
}
