using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
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

var planner = new HandlebarsPlanner(new HandlebarsPlannerOptions() { AllowLoops = true });

string location = "台北";
string goal = @$"根據用戶最近播放的音樂，推薦一場位於${location}的音樂會";

var concertPlan = await planner.CreatePlanAsync(kernel, goal);
// var result = await plan.InvokeAsync(kernel);

// output the plan result
// Console.WriteLine("Concert Plan:");
// Console.WriteLine(concertPlan);

var songSuggesterFunction = kernel.CreateFunctionFromPrompt(
    promptTemplate: @"根據用戶最近播放的音樂：
        {{$recentlyPlayedSongs}}
        從音樂庫中推薦一首歌曲給用戶：
        {{$musicLibrary}}
        ",
    functionName: "SuggestSong",
    description: "向用戶推薦一首歌曲"
);

kernel.Plugins.AddFromFunctions("SuggestSongPlugin", [songSuggesterFunction]);

var songSuggestPlan = await planner.CreatePlanAsync(kernel, @"根據用戶最近播放的歌曲，從音樂庫中向用戶推薦一首歌曲
");

// Console.WriteLine("Song Plan:");
// Console.WriteLine(songSuggestPlan);
string dir = Directory.GetCurrentDirectory();
string template = File.ReadAllText($"{dir}/handlebarsTemplate.txt");

var handlebarsPromptFunction = kernel.CreateFunctionFromPrompt(
    new()
    {
        Template = template,
        TemplateFormat = "handlebars"
    }, new HandlebarsPromptTemplateFactory()
);

var templateResult = await kernel.InvokeAsync(handlebarsPromptFunction,
    new() {
        { "location", location },
        { "suggestConcert", true }
    });

Console.WriteLine(templateResult);