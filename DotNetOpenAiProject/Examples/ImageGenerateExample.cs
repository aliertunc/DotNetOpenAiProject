using DotNetOpenAiProject;
using OpenAI.Images;

public class ImageGenerateExample
{
    public static void Run()
    {
        // API anahtarını ConfigReader sınıfından alma işlemi
        string apiKey = ConfigReader.ReadApiKeyFromConfig();

        // API anahtarı alınamazsa işlemi sonlandır
        if (string.IsNullOrEmpty(apiKey))
        {
            Console.WriteLine("API key not found in config.json");
            return;
        }

        ImageClient client = new(model: "dall-e-3", apiKey);
        string prompt = "Türkiyenin yarısında ekvator güneşli günü olsun. diğer yarısında kar sporları olsun. bu iki kareyi tek bir resimde birleştir. Bayrak da görülsün";

        ImageGenerationOptions options = new()
        {
            Quality = GeneratedImageQuality.Standard,
            Size = GeneratedImageSize.W512xH512,
            Style = GeneratedImageStyle.Vivid,
            ResponseFormat = GeneratedImageFormat.Bytes
        };
        Console.WriteLine("Fotoğraf oluşturuluyor..");
        GeneratedImage image = client.GenerateImage(prompt, options);
        BinaryData bytes = image.ImageBytes;

        using FileStream stream = File.OpenWrite($"{Guid.NewGuid()}.png");
        bytes.ToStream().CopyTo(stream);

        Console.WriteLine("Fotoğraf oluşturuldu...");

    }

}

