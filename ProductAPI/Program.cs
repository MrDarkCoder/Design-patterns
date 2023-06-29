// async request reply pattern.

using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDBContext>(options =>
        options.UseSqlite("Data Source=Request.db")
);

var app = builder.Build();

app.UseHttpsRedirection();

// start endpoint
app.MapPost("api/v1/products", async (AppDBContext context, ListingRequest listingRequest) =>
{
    if (listingRequest == null) return Results.BadRequest();

    listingRequest.RequestStatus = "ACCEPT";
    listingRequest.EstimatedCompletionTime = "2023-06-06:14:00:00";

    await context.ListingRequests.AddAsync(listingRequest);
    await context.SaveChangesAsync();

    return Results.Accepted($"api/v1/productstatus/{listingRequest.RequestId}", listingRequest);
});

// status endpoint
app.MapGet("api/v1/productstatus/{requestId}", (AppDBContext context, string requestId) =>
{
    var lisitingRequest = context.ListingRequests.FirstOrDefault(lr => lr.RequestId.Equals(requestId));

    if (lisitingRequest is null) return Results.NotFound();

    ListingStatusDTO listingStatus = new()
    {
        RequestStatus = lisitingRequest.RequestStatus,
        ResourceURL = string.Empty
    };

    if (lisitingRequest.RequestStatus!.ToUpper() == "COMPLETE")
    {
        listingStatus.ResourceURL = $"api/v1/products/{Guid.NewGuid()}";

        // return Results.Ok(listingStatus);

        return Results.Redirect($"https://localhost:5001/{listingStatus.ResourceURL}");
    }

    listingStatus.EstimatedCompletionTime = "2023-06-06:15:00:00";

    return Results.Ok(listingStatus);
});

// Final endpoint to return
app.MapGet("api/v1/products/{requestId}", (string requestId) =>
{
    return Results.Ok("This is where you would pass back the final result");
});

app.Run();
