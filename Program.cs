using ChatGPTCSharp;

var chatGpt = new ChatGPT();

Console.WriteLine("Digite um assunto para receber um texto e uma imagem gerada pelo ChatGPT.");
Console.WriteLine("");
Console.Write("Assunto: ");
var assunto = Console.ReadLine();

if (!string.IsNullOrWhiteSpace(assunto))
{
    var texto = await chatGpt.GenerateText(assunto);
    var imagem = await chatGpt.GenerateImage(assunto);

    Console.WriteLine("--");
    Console.WriteLine("RESULTADOS:");
    Console.WriteLine("--");
    Console.WriteLine($"Texto gerado: {texto}");
    Console.WriteLine($"Imagem gerada: {imagem}");
}