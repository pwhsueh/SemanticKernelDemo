using System.ComponentModel;
using Microsoft.SemanticKernel;

public class MusicConcertPlugin
{

    [KernelFunction, Description("獲取即將舉行的音樂會清單")]
    public static string GetTours()
    {
        string dir = Directory.GetCurrentDirectory();
        string content = File.ReadAllText($"{dir}/data/concertdates.txt");
        return content;
    }
}