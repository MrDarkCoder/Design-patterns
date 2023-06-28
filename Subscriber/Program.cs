using System.Net.Http.Json;
using Subscriber.Models;

Console.WriteLine("Press ESC to stop");
do
{
    HttpClient client = new HttpClient();
    Console.WriteLine("Listening...");
    while (!Console.KeyAvailable)
    {
        List<int> ackIds = await GetMessagesAsync(client);

        Thread.Sleep(2000);

        if (ackIds.Count > 0)
        {
            await AckMessagesAsync(client, ackIds);
        }
    }

} while (Console.ReadKey(true).Key != ConsoleKey.Escape);

static async Task<List<int>> GetMessagesAsync(HttpClient client)
{
    var ackIds = new List<int>();

    List<MessageReadDTO>? newMessages = new();

    try
    {
        newMessages = await client.GetFromJsonAsync<List<MessageReadDTO>>("https://localhost:5001/api/subscriptions/1/messages");
    }
    catch
    {
        return ackIds;
    }

    foreach(var msg in newMessages!)
    {
        Console.WriteLine($"{msg.Id} - {msg.TopicMessage} - {msg.MessageStatus}");

        ackIds.Add(msg.Id);
    }

    return ackIds;
}

static async Task AckMessagesAsync(HttpClient httpClient, List<int> ackIds)
{
    var response = await httpClient.PostAsJsonAsync("https://localhost:5001/api/subscriptions/1/messages", ackIds);
    var returnMessage = await response.Content.ReadAsStringAsync();

    Console.WriteLine(returnMessage);
}