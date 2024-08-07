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


string input = @"我有從6月1日到7月22日的假期。我想去希臘。我住在台灣。";

string prompt = @$"
<message role=""system"">指示：從使用者的請求中識別出發地和目的地以及日期
</message>

<message role=""user"">你能給我一份從西雅圖到東京的航班列表嗎？
我想從3月11日到3月18日旅行。</message>

<message role=""assistant"">西雅圖|東京|2024/03/11|2024/03/18
</message>

<message role=""user"">${input}</message>";
var result = await kernel.InvokePromptAsync(prompt);
Console.WriteLine(result);