using System.Text.Json.Serialization;

namespace SimpleTwitch.Data;

public class GameInfo {
    [JsonPropertyName("versions")] public List<Version> Versions { get; set; }
}