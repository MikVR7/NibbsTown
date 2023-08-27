using Firebase.Firestore;
using static NibbsTown.MapObjectStation;

namespace NibbsTown
{
    [FirestoreData]
    public class Description
    {
        public enum DescriptionType
        {
            None = 0,
            Text = 1,
            Image = 2,
        }
        [FirestoreProperty("dt")] public DescriptionType Type { get; set; }
        [FirestoreProperty("d")] public string Data { get; set; }
    }

    [FirestoreData]
    public class Rally
    {
        [FirestoreProperty("v")] public int Version { get; set; }
        [FirestoreProperty("n")] public string Name { get; set; }
        [FirestoreProperty("c")] public string Caption { get; set; }
        [FirestoreProperty("d")] public Description[] Descr { get; set; }
        [FirestoreProperty("gp")] public GPSPosition CenterPos { get; set; }
        [FirestoreProperty("z")] public float Zoom { get; set; }
        public Station[] Stations { get; set; }
        [FirestoreProperty("e")] public Description[] Endscreen { get; set; }
        internal bool Done { get; set; }
        internal string Key { get; set; }
    }
}
