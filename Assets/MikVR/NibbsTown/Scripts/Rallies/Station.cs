using Firebase.Firestore;
using static NibbsTown.MapObjectStation;

namespace NibbsTown
{
    [FirestoreData]
    public class Station
    {
        //[JsonIgnore] public string RallyID { get; set; }
        internal string Key { get; set; }
        internal double Distance { get; set; }
        internal StationState State { get; set; }
        [FirestoreProperty("i")] public int Index { get; set; }
        //[FirestoreProperty("ns")] public int[] NextStationIDs { get; set; }
        //[JsonProperty("fs")] public int FinalStation { get; set; }
        //[FirestoreProperty("if")] public bool IsFinalStation { get; set; }

        [FirestoreProperty("p")] public GPSPosition Pos { get; set; }
        [FirestoreProperty("n")] public string Name { get; set; }
        public RallyTask[] Tasks { get; set; }
    }
}
