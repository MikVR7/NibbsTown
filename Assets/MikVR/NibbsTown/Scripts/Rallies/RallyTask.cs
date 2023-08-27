using Firebase.Firestore;
using Newtonsoft.Json;

namespace NibbsTown
{
    [FirestoreData]
    public class RallyTask
    {
        public enum Type
        {
            None = 0,
            InfoScreen = 1,
            Task_Cloze = 100,
            Task_PicturePuzzle = 101,
        }

        //[JsonIgnore] public string StationID { get; set; }
        [JsonIgnore] internal string Key { get; set; }
        [FirestoreProperty("i")][JsonProperty("i")] public int Id { get; set; }
        [FirestoreProperty("t")][JsonProperty("t")] public Type TType { get; set; }
        [FirestoreProperty("d")][JsonProperty("d")] public Description[] Descr { get; set; }
    }
}
