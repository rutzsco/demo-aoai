using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel;
using Demo.AI.API.Services.Prompts;
using System.ComponentModel;

namespace Demo.AI.API.Services.Skills
{
    public sealed class SummarizeDocumentSkill
    {
        [KernelFunction("SummarizeDocument"), Description("Summarized the provided document text.")]
        public async Task<string> SummarizeDocumentyAsync(KernelArguments arguments,
                                                          Kernel kernel)
        {
            var chatGpt = kernel.Services.GetService<IChatCompletionService>();
            var chatHistory = new ChatHistory(PromptService.GetPromptByName(PromptService.SummarizeDocumentSystemPrompt));
            var userPrompt = PromptService.GetPromptByName(PromptService.SummarizeDocumentUserPrompt);
            var userMessage = await PromptService.RenderPromptAsync(kernel, userPrompt, arguments);
            chatHistory.AddUserMessage(userMessage);

            var answer = await chatGpt.GetChatMessageContentAsync(chatHistory, DefaultSettings.AIRequestSettings, kernel);
   
            return answer.Content;
        }
    }
}
