using Newtonsoft.Json.Linq;

namespace DotNetOpenAiProject
{
    public class ConfigReader
    {
        public static string ReadApiKeyFromConfig()
        {
            try
            {
                // Klasör yoluna göre config dosyasının tam yolunu ayarlayın
                string projectRoot = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;

                string configPath = Path.Combine(projectRoot, "DotNetOpenAiProject", "config.json");
                // C:\Users\GamePc\GithubProjects\DotNetOpenAiProject\DotNetOpenAiProject\config.json


                // config.json dosyasının var olup olmadığını kontrol et
                if (!File.Exists(configPath))
                {
                    Console.WriteLine("Config file not found.");
                    return null;
                }

                // Dosyayı oku ve JSON olarak parse et
                JObject config = JObject.Parse(File.ReadAllText(configPath));

                // "OpenAI" anahtarının ve "ApiKey" anahtarının olup olmadığını kontrol et
                if (config["OpenAI"]?["ApiKey"] == null)
                {
                    Console.WriteLine("API key not found in config file.");
                    return null;
                }

                // API anahtarını döndür
                return config["OpenAI"]["ApiKey"].ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading API key from config file: {ex.Message}");
                return null;
            }
        }
    }
}
