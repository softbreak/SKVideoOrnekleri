using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

string modelId = "gpt-4o";
string endpoint = "https://newskworkshop.openai.azure.com/";
string apiKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY") ?? throw new InvalidOperationException("API key bulunamadı");

IKernelBuilder builder = Kernel.CreateBuilder();
builder.AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);

Kernel kernel = builder.Build();

IChatCompletionService chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

AzureOpenAIPromptExecutionSettings settings = new()
{
    Temperature = 0.3
};

ChatHistory chatHistory = new();

chatHistory.AddSystemMessage("""

    Sen kullanıcıya doğal, kısa ve anlaşılır şekilde konuşan yardımcı bir asistansın.Kullanıcının önceki mesajlarını dikkate alarak cevap ver...

    """);


Console.WriteLine("--- Chat Histroy Örneği ---");
Console.WriteLine("Cıkmak icin bos bırakıp enter'a basın");

while(true)
{
    Console.WriteLine("Sen : ");
    string? userInput = Console.ReadLine();
    Console.WriteLine("Cevap:");

    if (string.IsNullOrEmpty(userInput)) break;

    chatHistory.AddUserMessage(userInput);

    ChatMessageContent? response = await chatCompletionService.GetChatMessageContentAsync(chatHistory,settings);

    string assistantMessage = response.Content ?? "";

    Console.WriteLine($"AI: {assistantMessage}");
    chatHistory.AddAssistantMessage(assistantMessage);  

    //ChatHistory,modelin kalıcı hafızası degildir...Biz her istekte konuşma geçmişini modele geri göndermeliyiz...
}

