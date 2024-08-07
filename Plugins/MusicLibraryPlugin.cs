using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.SemanticKernel;

public class MusicLibraryPlugin
{
    [KernelFunction,
    Description("獲取用戶最近播放的音樂清單")]
    public static string GetRecentPlays()
    {
        string dir = Directory.GetCurrentDirectory();
        string content = File.ReadAllText($"{dir}/data/recentlyplayed.txt");
        return content;
    }

    [KernelFunction, Description("將一首歌曲添加到最近播放清單")]
    public static string AddToRecentlyPlayed(
    [Description("歌手的名字")] string artist,
    [Description("專輯的名稱")] string song,
    [Description("歌曲類型")] string genre)
    {
        // Read the existing content from the file
        string filePath = "data/recentlyplayed.txt";
        string jsonContent = File.ReadAllText(filePath);
        var recentlyPlayed = (JsonArray)JsonNode.Parse(jsonContent);

        var newSong = new JsonObject
        {
            ["title"] = song,
            ["artist"] = artist,
            ["genre"] = genre
        };

        recentlyPlayed.Insert(0, newSong);
        File.WriteAllText(filePath, JsonSerializer.Serialize(recentlyPlayed,
            new JsonSerializerOptions { WriteIndented = true }));

        return $"新增 '{song}' 到最近播放清單";
    }
}