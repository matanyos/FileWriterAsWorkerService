namespace FileWriterServiceWorker;

internal class FileWriter
{
    public static async Task StartOperation(string outputFilePath, string value,ILogger? logger)
    {
        try
        {
            await using var writer = new StreamWriter(File.Open(outputFilePath, FileMode.Append));
            await writer.WriteLineAsync(value);

            await Task.Delay(3000);
        }
        catch 
        {
            logger!.LogInformation("Could not write to file");
        }
    }
}
