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

builder.Plugins.AddFromType<ConversationSummaryPlugin>();

var kernel = builder.Build();

string input = @" Alice: Hi Bob, how are you doing today?
                Bob: I'm doing great, Alice! How about you?
                Alice: I'm good too, thanks for asking. Have you finished the report yet?
                Bob: Yes, I finished it last night. I'll send it to you shortly.
                Alice: Awesome, thanks!";

var result = await kernel.InvokeAsync(
    "ConversationSummaryPlugin",
    "SummarizeConversation",
    new() { { "input", input } });

Console.WriteLine(result);