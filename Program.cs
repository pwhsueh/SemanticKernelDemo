using Microsoft.SemanticKernel;
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
var plugins = kernel.CreatePluginFromPromptDirectory("Prompts");
string input = "G, C";

var result = await kernel.InvokeAsync(
    plugins["SuggestChords"],
    new() { { "startingChords", input } });

Console.WriteLine(result);