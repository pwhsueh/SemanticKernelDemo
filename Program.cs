using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Plugins.Core;

const string api_Key = "";


// Create kernel
var builder = Kernel.CreateBuilder();
builder.AddOpenAIChatCompletion(
    apiKey: api_Key,
    modelId: "gpt-4o" // optional
);

#pragma warning disable SKEXP0050 


var kernel = builder.Build();
var prompts = kernel.ImportPluginFromPromptDirectory("Prompts/TravelPlugins");

ChatHistory history = [];
string input = @"我正在計劃和女朋友一起的紀念交往周年旅行。我們喜歡徒步旅行、山脈和海灘。我們的旅行預算是台幣$50000。";

var result = await kernel.InvokeAsync<string>(prompts["SuggestDestinations"],
    new() { { "input", input } });

Console.WriteLine(result);
history.AddUserMessage(input);
history.AddAssistantMessage(result);

Console.WriteLine("您想去哪裡？");
input = Console.ReadLine();

result = await kernel.InvokeAsync<string>(prompts["SuggestActivities"],
    new() {
        { "history", history },
        { "destination", input },
    }
);
Console.WriteLine(result);