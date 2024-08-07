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


string language = "英文";
string history = @"我和孩子一起旅行，其中一個孩子對花生過敏。";

string prompt = @$"考慮旅行者的背景：
    ${history}

    創建一個列表，列出旅行者在${language}中可能會發現有用的短語和詞彙。

    按類別分組短語。包括常見的方向詞。以以下格式顯示短語：
    你好 - Bonjour [bohn-zhoor]";
var result = await kernel.InvokePromptAsync(prompt);
Console.WriteLine(result);