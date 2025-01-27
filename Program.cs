using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

class Program
{
    private static readonly object FileLock = new object();
    private static int LineCount = 0;
    private static string FilePath = string.Empty;

    static async Task Main(string[] args)
    {
        try
        {
            // Load configuration
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            FilePath = configuration["FileSettings:FilePath"] ?? throw new InvalidOperationException("File path is not configured.");

            var directoryPath = Path.GetDirectoryName(FilePath);
            if (directoryPath == null)
            {
                throw new InvalidOperationException("The directory path could not be determined.");
            }
            Console.WriteLine(FilePath);
            // Ensure the log directory exists
            Directory.CreateDirectory(directoryPath);

            // Initialize the file
            await InitializeFileAsync();

            // Create and start threads
            Task[] tasks = new Task[10];
            for (int i = 0; i < 10; i++)
            {
                int threadId = i; // Local copy for lambda capture
                tasks[i] = WriteToFileAsync(threadId);
            }

            // Wait for all threads to complete
            await Task.WhenAll(tasks);

            Console.WriteLine(Path.GetDirectoryName(Path.GetFullPath(FilePath)));

            Console.WriteLine("All threads have completed. Press any key to exit.");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private static async Task InitializeFileAsync()
    {
        try
        {
            using (var writer = new StreamWriter(FilePath, false))
            {
                await writer.WriteLineAsync($"0, 0, {GetTimestamp()}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to initialize file: {ex.Message}");
            throw;
        }
    }

    private static async Task WriteToFileAsync(int threadId)
    {
        try
        {
            for (int i = 0; i < 10; i++)
            {
                await Task.Run(() =>
                {
                    lock (FileLock)
                    {
                        LineCount++;
                        using (var writer = new StreamWriter(FilePath, true))
                        {
                            writer.WriteLineAsync($"{LineCount}, {threadId}, {GetTimestamp()}").Wait();
                        }
                    }
                });
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Thread {threadId} encountered an error: {ex.Message}");
        }
    }

    private static string GetTimestamp()
    {
        return DateTime.Now.ToString("HH:mm:ss.fff");
    }
}
