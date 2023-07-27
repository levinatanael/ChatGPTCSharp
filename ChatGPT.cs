using OpenAI;
using OpenAI.Managers;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;
using System.Configuration;

namespace ChatGPTCSharp
{
    public class ChatGPT
    {
        public OpenAIService Service { get; private set; }

        public ChatGPT()
        {
            //Conexão com o ChatGPT
            Service = new OpenAIService(new OpenAiOptions()
            {
                //Essa KEY que você irá gerar no https://platform.openai.com/
                //Menu Personal > View API Keys > Create new secrete key
                ApiKey = ConfigurationManager.AppSettings["ApiKeyChatGpt"]
            });
        }

        public async Task<string> GenerateText(string prompt)
        {
            var completionResult = await Service.Completions.CreateCompletion(new CompletionCreateRequest
            {
                Prompt = prompt,
                Model = Models.TextDavinciV3
            });

            if (completionResult.Successful)
            {
                var result = completionResult.Choices.FirstOrDefault();
                var text = "Nenhum resultado encontrado.";
                if (result != null)
                {
                    text = result.Text;
                }
                return text;
            }

            return $"Erro: #{completionResult.Error?.Code}: {completionResult.Error?.Message}";
        }

        public async Task<string> GenerateImage(string prompt)
        {
            var imageResult = await Service.Image.CreateImage(new ImageCreateRequest
            {
                Prompt = prompt,
                N = 1,
                Size = StaticValues.ImageStatics.Size.Size256,
                //Neste caso, optei por receber a URL da imagem, mas existem outros tipos de retorno que você poderá utilizar
                ResponseFormat = StaticValues.ImageStatics.ResponseFormat.Url
            }); ;

            if (imageResult.Successful)
            {
                var image = imageResult.Results.FirstOrDefault();
                var url = "Nenhuma imagem encontrada.";
                if (image != null)
                {
                    url = image.Url;
                }
                return url;
            }

            return $"Erro: #{imageResult.Error?.Code}: {imageResult.Error?.Message}";
        }
    }
}
