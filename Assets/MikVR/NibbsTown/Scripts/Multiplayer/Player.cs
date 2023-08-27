using Newtonsoft.Json;

namespace NibbsTown
{
    public class Player
    {
        [JsonProperty("dbk")] public string Key { get; set; }
        [JsonProperty("n")] public string Name { get; set; }
        [JsonProperty("gp")] public GPSPosition GPSPosition { get; set; }
        [JsonProperty("g")] public string Group { get; set; }
    }
}
