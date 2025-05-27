using System.Text.Json.Serialization;

public class CountryImagesResponse
{
    [JsonPropertyName("error")]
    public bool Error { get; set; }

    [JsonPropertyName("msg")]
    public string Message { get; set; }

    [JsonPropertyName("data")]
    public List<CountryData> Data { get; set; }
}

public class CountryData
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("flag")]
    public string Flag { get; set; }
}
public class CitiesResponse
{
    [JsonPropertyName("error")]
    public bool Error { get; set; }

    [JsonPropertyName("msg")]
    public string Message { get; set; }

    [JsonPropertyName("data")]
    public List<string> Data { get; set; }
}
