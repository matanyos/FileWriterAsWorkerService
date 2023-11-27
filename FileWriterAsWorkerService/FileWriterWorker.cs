using System.IO;

namespace FileWriterServiceWorker;

public class FileWriterWorker : BackgroundService
{
    private readonly ILogger<FileWriterWorker> logger;
    private readonly InMemoryRequestQueue requestQueue;
    private readonly string? outputFilePath;

    public FileWriterWorker(ILogger<FileWriterWorker> logger, InMemoryRequestQueue requestQueue)
    {
        this.logger = logger;
        this.requestQueue = requestQueue;

        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        var configuration = builder.Build();
        outputFilePath = configuration["outputFile"];
    }
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var operationInfo = requestQueue.Dequeue();
            if (operationInfo == null)
            {
                try
                {
                    await FileWriter.StartOperation(outputFilePath!, "Something", logger);
                }
                catch
                {
                    logger.LogInformation("File is being used");
                }
            }
            else
            {
                await FileWriter.StartOperation(operationInfo.Path, operationInfo.Value, logger);
            }

            await Task.Delay(10000, cancellationToken); 
        }
    }
}