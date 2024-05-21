using Microsoft.SemanticKernel;

namespace Demo.AI.API
{

    public static class DefaultSettings
    {
        public static int MaxRequestTokens = 6000;
        public static int KNearestNeighborsCount = 3;

        public static PromptExecutionSettings AIRequestSettings = new()
        {
            ExtensionData = new Dictionary<string, object>()
        {
            { "max_tokens", 1024 },
            { "temperature", 0.0 },
            { "top_p", 1 },
        }
        };

        public static string SummarizeDocumentPluginName = "SummarizeDocumentPlugin";
        public static string SummarizeDocumentFunctionName = "SummarizeDocument";

    }
}
