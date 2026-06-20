using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
using System.Text;

string modelId = "gpt-4o";
string endpoint = "https://newskworkshop.openai.azure.com/";
string apiKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY") ?? throw new InvalidOperationException("API key bulunamadı");

IKernelBuilder builder = Kernel.CreateBuilder();

builder.AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);

Kernel kernel = builder.Build();

IChatCompletionService chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

//Asistanın davanış kuralları

AzureOpenAIPromptExecutionSettings settings = new()
{
    //Persona'yı ayarlamak...
    ChatSystemPrompt = """

     Sen resmi yazışmalar konusunda uzman bir asistansın..

     Görevin, kullanıcının günlük hayatta yaşadığı problemi resmi,kibar ve anlaşılır bir şikayet metnine dönüştürmektir...

     Kurallar:

     -Resmi ama sade bir Türkçe kullan.
     -Gereksiz uzun yazma.
     -Hakaret,tehdit veya agresif bir ifade kullanma.
     -Hukuki tavsiye verme.
     -Kesin yasal hüküm kurma.
     -Metni hazır gönderilebilir formatta oluştur.
     -Metnin sonunda kısa ve net bir talep cümlesi olsun.

    """,
    Temperature = 0.3 //Yaratıcılık düzeyi..0'a yaklasınca deterministik, 1'e yaklasınca daha yaratıcı sonuclar verir.Resmi dilekce oldugu icin düsük bir deger seciyoruz...
};

Console.WriteLine("---Resmi mesaj/Sikayet metni asistanı---");
Console.WriteLine();

while (true)
{
    Console.WriteLine("Mesajı göndereceginiz kurum veya kişiyi yazınız");
    Console.WriteLine("Cıkmak icin bos bırakıp entera basınız");
    Console.Write("> ");

    string? recipient = Console.ReadLine();

    if (string.IsNullOrEmpty(recipient)) break;

    Console.WriteLine();

    Console.WriteLine("Mesajın tonunu secin : \n(1) Resmi\n(2) Kibar\n(3) Ciddi ama saygılı");

    string? toneSelection = Console.ReadLine();

    string tone = toneSelection switch
    {
        "1" => "Resmi",
        "2" => "Kibar",
        "3" => "Ciddi ama saygılı",
        _ => "Resmi"
    };

    Console.WriteLine();

    Console.WriteLine("Yasadıgınız problemi kısaca yazınız");
    Console.WriteLine("Cıkmak icin bos bırakıp enter'a basınız");

    Console.Write(">  ");

    string? userInput = Console.ReadLine();

    if(string.IsNullOrEmpty(userInput)) break;

    string prompt = $"""
        
        Muhatap : {recipient}

        Mesaj Tonu : {tone}

        Kullanıcının yaşadıgı problem : {userInput}

        Yukarıdaki bilgiler dogrultusunda uygum bir mesaj olustur.

        Format:
        Konu:
        Açıklama:
        Talep:


        """;

    Console.WriteLine();
    Console.WriteLine("Metin hazırlanıyor...");

    var response = await chatCompletionService.GetChatMessageContentAsync(prompt, settings);

    Console.WriteLine(response.Content);
    Console.WriteLine("--------------------------------");
}