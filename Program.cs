using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Planning.Handlebars;
using Microsoft.SemanticKernel.Plugins.Core;
using Microsoft.SemanticKernel.PromptTemplates.Handlebars;

const string api_Key = "";


// Create kernel
var builder = Kernel.CreateBuilder();
builder.AddOpenAIChatCompletion(
    apiKey: api_Key,
    modelId: "gpt-4o" // optional
);

#pragma warning disable SKEXP0060

var kernel = builder.Build();
kernel.ImportPluginFromType<MusicLibraryPlugin>();
kernel.ImportPluginFromType<MusicConcertPlugin>();
kernel.ImportPluginFromPromptDirectory("Prompts");

OpenAIPromptExecutionSettings settings = new()
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
};

string prompt = @"我住在台北。根據我最近播放的歌曲和即將舉行的音樂會清單，您推薦哪場音樂會？
";

var result = await kernel.InvokePromptAsync(prompt, new(settings));

Console.WriteLine(result);