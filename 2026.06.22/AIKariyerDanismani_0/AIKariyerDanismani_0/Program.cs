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
    Temperature = 0.5
};

ChatHistory chatHistory = new();

chatHistory.AddSystemMessage("""

    Sen deneyimli bir kariyer ve gelişim danışmanısın.

    Görevin, kullanıcının ilgi alanlarını,yeteneklerini,hedeflerini ve
    önceki konuşmalarını dikkate alarak ona uygun kariyer ve öğrenme önerileri sunmaktır...

    Kurallar:
    -Kullanıcının önceki mesajlarını dikkate al.
    -İlgi alanları ile kariyer seçenekleri arasında bağlantı kur.
    -Gereksiz uzun cevap verme.
    -Gerçekçi ve uygulanabilir öneriler sun.
    -Kullanıcı isterse öğrenme planı oluştur.
    -Kullanıcıyı motive et ancak gerçeklerden koparma.
    -Cevaplarını sade ve anlaşılır Türkçe ile ver.

    """);


Console.WriteLine("--- AI Destekli Kariyer ve İlgi Alanı Danışmanı ---");
Console.WriteLine("Kendinizden , ilgi alanlarınızdan ve hedeflerinizden bahsedin. Çıkmak için boş bırakıp enter'a basın.");

while (true)
{
    Console.WriteLine("Sen : ");
    string? userInput = Console.ReadLine();
    Console.WriteLine("Cevap:");

    if (string.IsNullOrEmpty(userInput)) break;

    chatHistory.AddUserMessage(userInput);

    ChatMessageContent? response = await chatCompletionService.GetChatMessageContentAsync(chatHistory, settings);

    string assistantMessage = response.Content ?? "";

    Console.WriteLine($"AI: {assistantMessage}");
    chatHistory.AddAssistantMessage(assistantMessage);

    //ChatHistory,modelin kalıcı hafızası degildir...Biz her istekte konuşma geçmişini modele geri göndermeliyiz...
}


