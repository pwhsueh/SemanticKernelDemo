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
kernel.ImportPluginFromType<MusicLibraryPlugin>();

string prompt = @"以下是用戶可用的音樂清單：
    {{MusicLibraryPlugin.GetMusicLibrary}} 

    以下是用戶最近播放的音樂清單：
    {{MusicLibraryPlugin.GetRecentPlays}}

   根據他們最近播放的音樂，從列表中推薦下一首播放的歌曲.請列出1首歌曲名稱"
;

var result = await kernel.InvokePromptAsync(prompt);
Console.WriteLine(result);