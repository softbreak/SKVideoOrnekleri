# Microsoft Semantic Kernel — .NET Video Örnekleri

Bu repository, Softbreak YouTube kanalında Microsoft Semantic Kernel ve Azure OpenAI üzerine hazırlanan videolardaki örnek projeleri içerir.

Amaç yalnızca API çağrısı yapmak değil; Semantic Kernel ile bir sohbet uygulamasının adım adım nasıl geliştirildiğini çalışan C# örnekleri üzerinden göstermektir.

Örnekler kronolojik olarak ilerler. Her çalışma bir önceki konunun üzerine yeni bir yetenek ekler.

---

## İçerik

| Tarih | Örnek | Konu |
|---|---|---|
| 12.06.2026 | [SemanticKernelIntro_0](./2026.06.12/SemanticKernelIntro_0) | Semantic Kernel ile Azure OpenAI chat completion temel kullanımı |
| 16.06.2026 | [KernelMessageAssistant](./2026.06.16/KernelMessageAssistant) | System prompt, persona, temperature ve yapılandırılmış AI asistanı |
| 21.06.2026 | [SKChatHistory_0](./2026.06.21/SKChatHistory_0) | ChatHistory ile konuşma geçmişinin modele yeniden gönderilmesi |
| 22.06.2026 | [AIKariyerDanismani_0](./2026.06.22/AIKariyerDanismani_0) | Konuşma geçmişini kullanan AI destekli kariyer danışmanı |

---

## 1 — Semantic Kernel'e Giriş

Klasör:

`2026.06.12/SemanticKernelIntro_0`

Bu örnekte:

- `Kernel.CreateBuilder()` ile Semantic Kernel oluşturulur.
- Azure OpenAI chat completion servisi eklenir.
- `IChatCompletionService` kullanılır.
- Kullanıcıdan terminal üzerinden prompt alınır.
- Model cevabı console üzerinde gösterilir.

Bu proje serinin en temel örneğidir.

---

## 2 — Mesaj ve Şikayet Asistanı

Klasör:

`2026.06.16/KernelMessageAssistant`

Bu örnekte temel chat kullanımına ek olarak:

- system prompt ile asistana görev ve davranış kuralları verilir,
- persona tanımlanır,
- `Temperature` ayarlanır,
- kullanıcıdan muhatap, ton ve problem bilgileri alınır,
- çıktı belirli bir formatta üretilir.

Örnek, genel amaçlı bir modeli belirli bir görev için yönlendirme mantığını gösterir.

---

## 3 — ChatHistory

Klasör:

`2026.06.21/SKChatHistory_0`

Bu örnekte `ChatHistory` kullanılarak çok turlu sohbet oluşturulur.

Her kullanıcı mesajı ve model cevabı konuşma geçmişine eklenir.

Önemli nokta:

`ChatHistory` modelin kalıcı hafızası değildir.

Uygulama, konuşma geçmişini her yeni istekte tekrar modele gönderir.

---

## 4 — AI Destekli Kariyer Danışmanı

Klasör:

`2026.06.22/AIKariyerDanismani_0`

Bu örnek önceki `ChatHistory` yapısını gerçek bir senaryoya uygular.

Asistan:

- kullanıcının ilgi alanlarını,
- hedeflerini,
- önceki mesajlarını

dikkate alarak kariyer ve öğrenme önerileri üretir.

Böylece temel chat completion örneğinden, konuşma bağlamını kullanan görev odaklı bir asistana geçilir.

---

## Teknolojiler

Projelerde kullanılan temel teknoloji ve paketler:

- C#
- .NET 9
- Microsoft Semantic Kernel
- Microsoft Semantic Kernel Azure OpenAI Connector
- Azure OpenAI
- Console Applications

Örneklerde Semantic Kernel paketlerinin `1.77.0` sürümü kullanılmaktadır.

---

## Gereksinimler

Örnekleri çalıştırmak için:

- .NET 9 SDK
- Azure OpenAI erişimi
- çalışan bir Azure OpenAI deployment
- Azure OpenAI API key

gereklidir.

API key kaynak kod içerisinde tutulmaz.

Uygulamalar anahtarı şu environment variable üzerinden okur:

`AZURE_OPENAI_API_KEY`

WSL / Linux üzerinde örnek:

    export AZURE_OPENAI_API_KEY="your-api-key"

Ardından ilgili proje klasöründe:

    dotnet restore
    dotnet run

komutları kullanılabilir.

---

## Repository Yapısı

Her video örneği yayınlandığı tarihe ait klasör altında tutulur.

Bu yapı geçmiş YouTube içerikleriyle repository arasındaki bağlantının korunmasını sağlar.

Yeni örnekler geldikçe repository aynı yapı içerisinde genişletilebilir.

---

## Softbreak

📺 [YouTube](https://youtube.com/@softbreak)

🌐 [softbreak.net](https://softbreak.net)
