using Demo.AI.API.Services.Skills;
using Microsoft.SemanticKernel;

namespace Demo.AI.API
{
    internal static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddAzureServices(this IServiceCollection services)
        {
            services.AddSingleton<Kernel>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var deployedModelName = config["AOAIChatGptDeployment"];
                var azureOpenAiServiceEndpoint = config["AOAIServiceEndpoint"];
                var azureOpenAiServiceKey = config["AOAIServiceKey"];

                ArgumentNullException.ThrowIfNullOrEmpty(deployedModelName);
                ArgumentNullException.ThrowIfNullOrEmpty(azureOpenAiServiceEndpoint);
                ArgumentNullException.ThrowIfNullOrEmpty(azureOpenAiServiceKey);


                // Build Plugins
                var summarizeDocumentSkill = new SummarizeDocumentSkill();


                // Build Kernels
                Kernel kernel = Kernel.CreateBuilder()
                   .AddAzureOpenAIChatCompletion(deployedModelName, azureOpenAiServiceEndpoint, azureOpenAiServiceKey)
                   .Build();

                kernel.ImportPluginFromObject(summarizeDocumentSkill, DefaultSettings.SummarizeDocumentPluginName);

                return kernel;
            });

            return services;
        }
    }
}