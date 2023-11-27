using FileWriterServiceWorker;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseWindowsService();

builder.Services.AddSingleton<InMemoryRequestQueue>();
builder.Services.AddHostedService<FileWriterWorker>();

var app = builder.Build();

app.MapGet("/Invoke", Invoke);

app.Run();

static async Task<IResult> Invoke(IConfiguration configuration, InMemoryRequestQueue messageQueue)
{
    var path = configuration["outputFile"];

    try
    {
        await FileWriter.StartOperation(path!, "Something HTTP", null);
        return Results.Ok("done");
    }
    catch
    {
        messageQueue.Enqueue(new OperationInfo(path!, "Something HTTP from queue"));

        return Results.Ok("queued");
    }
}