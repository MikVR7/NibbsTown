using Newtonsoft.Json;

namespace NibbsTown
{
    public class Location
    {
        [JsonProperty("lat")] public double latitude { get; set; } = 0d; // horizontal
        [JsonProperty("long")] public double longitude { get; set; } = 0d; // vertical
        [JsonProperty("time")] public string time { get; set; } = string.Empty;
    }

    public class Locations
    {
        [JsonProperty("location")] public Location[] locations { get; set; } = null;
    }
}
