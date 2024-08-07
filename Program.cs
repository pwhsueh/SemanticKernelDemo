using Microsoft.SemanticKernel;

const string api_Key = "";


// Create kernel
var builder = Kernel.CreateBuilder();
builder.AddOpenAIChatCompletion(
    apiKey: api_Key,
    modelId: "gpt-4o" // optional
);
var kernel = builder.Build();
var result = await kernel.InvokePromptAsync(
        "請給我一份包含雞蛋和起司的早餐食物清單。");
Console.WriteLine(result);