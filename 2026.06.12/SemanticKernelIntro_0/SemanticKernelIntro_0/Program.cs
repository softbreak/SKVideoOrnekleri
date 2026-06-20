using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using System.Text;

string modelId = "gpt-4o";
string endpoint = "https://newskworkshop.openai.azure.com/";
string apiKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY") ?? throw new InvalidOperationException("API key bulunamadı");

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;


IKernelBuilder builder = Kernel.CreateBuilder();

builder.AddAzureOpenAIChatCompletion(modelId,endpoint, apiKey);

Kernel kernel = builder.Build();

IChatCompletionService chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

Console.WriteLine("Bir şeyler yazın...Çıkmak icin bos bırakıp entera basın");

while (true)
{
    Console.WriteLine("Sen: ");

    string? prompt = Console.ReadLine();

    Console.WriteLine("Cevap:");

    if (string.IsNullOrEmpty(prompt)) break;

    ChatMessageContent? response = await chatCompletionService.GetChatMessageContentAsync(prompt);

    Console.WriteLine(response.Content);
}
