using System.Text.Json.Serialization;

namespace SimpleTwitch.Data;

public class Version {
    [JsonPropertyName( "id" )] public string Id { get; set; }

    [JsonPropertyName( "image_url_1x" )] public string ImageUrl1x { get; set; }

    [JsonPropertyName( "image_url_2x" )] public string ImageUrl2x { get; set; }

    [JsonPropertyName( "image_url_4x" )] public string ImageUrl4x { get; set; }

    [JsonPropertyName( "title" )] public string Title { get; set; }

    [JsonPropertyName( "description" )] public string Description { get; set; }

    [JsonPropertyName( "click_action" )] public string ClickAction { get; set; }

    [JsonPropertyName( "click_url" )] public string ClickUrl { get; set; }
}