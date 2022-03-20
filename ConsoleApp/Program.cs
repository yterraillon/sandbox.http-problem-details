// https://codeopinion.com/handling-http-api-errors-with-problem-details/

using System.Net.Http.Json;
using ConsoleApp;

Console.WriteLine("Hello, World!");

var httpClient = new HttpClient(new ProblemDetailsHttpMessageHandler());
try
{
    var result = await httpClient.PostAsJsonAsync("https://localhost:7015/Demo", new { });
}
catch (Exception exception)
{
    Console.WriteLine(exception.Message);
}