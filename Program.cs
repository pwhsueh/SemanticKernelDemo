using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Planning.Handlebars;
using Microsoft.SemanticKernel.Plugins.Core;

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

var planner = new HandlebarsPlanner(new HandlebarsPlannerOptions() { AllowLoops = true });

string location = "高雄";
string goal = @$"根據用戶最近播放的音樂，推薦一場位於${location}的音樂會";

var plan = await planner.CreatePlanAsync(kernel, goal);
var result = await plan.InvokeAsync(kernel);

Console.WriteLine($"Results: {result}");