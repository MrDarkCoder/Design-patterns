using Microsoft.EntityFrameworkCore;

using MessageBroker.Data;
using MessageBroker.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DBContext>(
    options => options.UseSqlite("Data Source=MessageBroker.db")
    );

var app = builder.Build();

app.UseHttpsRedirection();

// Create Topic
app.MapPost("api/topics", async (DBContext context, Topic topic) =>
{
    await context.Topics.AddAsync(topic);

    await context.SaveChangesAsync();

    return Results.Created($"api/topics/{topic.Id}", topic);
});

// Return all topics
app.MapGet("api/topics", async (DBContext context) =>
{
    return Results.Ok(await context.Topics.ToListAsync());
});

// Publish message
app.MapPost("api/topics/{id}/message", async (DBContext context, int id, Message message) =>
{
    bool isTopicExist = await context.Topics.AnyAsync(tp => tp.Id.Equals(id));

    if (!isTopicExist) return Results.NotFound("Topic not found");

    var subs = context.Subscriptions.Where(s => s.TopicId.Equals(id));

    if (!subs.Any()) return Results.NotFound("No subscribers found");

    foreach (var sub in subs)
    {
        Message msg = new()
        {
            TopicMessage = message.TopicMessage,
            SubscriptionId = sub.Id,
            ExpriesAfter = message.ExpriesAfter,
            MessageStatus = message.MessageStatus,
        };

        await context.Messages.AddAsync(msg);
    }

    await context.SaveChangesAsync();

    return Results.Ok("Message has been published");
});

// Create subscriptions
app.MapPost("api/topics/{id}/subscriptions", async (DBContext context, int id, Subscription subscription) =>
{
    bool isTopicExist = await context.Topics.AnyAsync(tp => tp.Id.Equals(id));

    if (!isTopicExist) return Results.NotFound("Topic not found");

    subscription.TopicId = id;

    await context.Subscriptions.AddAsync(subscription);

    await context.SaveChangesAsync();

    return Results.Created($"api/topics/{id}/subscriptions/{subscription.Id}", subscription);
});

// Get subscriber messages
app.MapGet("api/subscriptions/{id}/messages", async (DBContext context, int id) =>
{
    if (!await context.Subscriptions.AnyAsync(s => s.Id.Equals(id))) return Results.NotFound("Subscription not found");

    var messages = await context.Messages.Where(m => m.SubscriptionId.Equals(id) && m.MessageStatus != "SENT").ToListAsync();

    if (messages.Count == 0) return Results.NotFound("No new messages");

    foreach (var msg in messages)
    {
        msg.MessageStatus = "REQUESTED";
    }

    await context.SaveChangesAsync();

    return Results.Ok(messages);
});

// Ack messages
app.MapPost("api/subscriptions/{id}/messages", async (DBContext context, int id, int[] confirmations) =>
{
    if (!await context.Subscriptions.AnyAsync(s => s.Id.Equals(id))) return Results.NotFound("Subscription not found");

    if (confirmations.Length <= 0) return Results.BadRequest();


    int counter = 0;
    foreach(int i in confirmations)
    {
        var msg = await context.Messages.FirstOrDefaultAsync(m => m.Id.Equals(i));

        if(msg != null)
        {
            msg.MessageStatus = "SENT";
            await context.SaveChangesAsync();
            counter++;
        }
    }

    return Results.Ok($"Acknowledged {counter}/{confirmations.Length} messages");
});

app.Run();

/* 
SELECT
'data source=' + @@SERVERNAME +
';initial catalog=' + DB_NAME() +
CASE type_desc
    WHEN 'WINDOWS_LOGIN' 
            THEN ';trusted_connection=true'
        ELSE
            ';user id=' + SUSER_NAME()
    END
FROM sys.server_principals
WHERE name = suser_name()
*/