using System.Collections.Generic;
using System;
using Firebase.Firestore;
using UnityEngine;

namespace NibbsTown
{
    internal class DatabaseRallies : ARealtimeDatabase
    {
        internal static EventIn_DeleteAllRalliesFromDB EventIn_DeleteAllRalliesFromDB = new EventIn_DeleteAllRalliesFromDB();
        internal static EventIn_AddRallyToDB EventIn_AddRallyToDB = new EventIn_AddRallyToDB();

        internal static EventIn_SaveRallyWhole EventIn_SaveRallyWhole = new EventIn_SaveRallyWhole();
        internal static EventInOut_LoadDBRalliesAll EventInOut_LoadDBRalliesAll = new EventInOut_LoadDBRalliesAll();
        internal static EventInOut_LoadDBStations EventInOut_LoadDBStations = new EventInOut_LoadDBStations();
        internal static EventInOut_LoadDBTasks EventInOut_LoadDBTasks = new EventInOut_LoadDBTasks();

        private static readonly string DB_COLLECTION_RALLIES = "rallies";
        private static readonly string DB_COLLECTION_STATIONS= "stations";
        private static readonly string DB_COLLECTION_TASKS = "tasks";

        internal override void Init(FirebaseFirestore firebaseFirestore) {
            base.Init(firebaseFirestore);

            EventIn_DeleteAllRalliesFromDB.AddListenerSingle(DeleteAllRalliesFromDB);
            EventIn_AddRallyToDB.AddListenerSingle(AddRallyToDB);

            EventInOut_LoadDBRalliesAll.AddListenerSingle(LoadDBRalliesAll);
            EventInOut_LoadDBStations.AddListenerSingle(LoadDBStations);
            EventInOut_LoadDBTasks.AddListenerSingle(LoadDBTasks);
        }

        private async void AddRallyToDB(Rally rally)
        {
            DocumentReference rallyRef = await firebaseFirestore.Collection(DB_COLLECTION_RALLIES).AddAsync(rally);
            CollectionReference stationsCollection = rallyRef.Collection(DB_COLLECTION_STATIONS);
            foreach (Station station in rally.Stations)
            {
                DocumentReference stationRef = await stationsCollection.AddAsync(station);
                CollectionReference tasksCollection = stationRef.Collection(DB_COLLECTION_TASKS);
                for(int i = 0; i < station.Tasks.Length; i++)
                {
                    await tasksCollection.AddAsync(station.Tasks[i]);
                }
                //foreach (RallyTask task in station.Tasks)
                //{
                //    await tasksCollection.AddAsync(task);
                //}
            }
        }

        private async void DeleteAllRalliesFromDB()
        {
            QuerySnapshot rallySnapshot = await firebaseFirestore.Collection(DB_COLLECTION_RALLIES).GetSnapshotAsync();
            foreach (DocumentSnapshot rallyDocument in rallySnapshot.Documents)
            {
                QuerySnapshot stationSnapshot = await rallyDocument.Reference.Collection(DB_COLLECTION_STATIONS).GetSnapshotAsync();
                foreach (DocumentSnapshot stationDocument in stationSnapshot.Documents)
                {
                    QuerySnapshot taskSnapshot = await stationDocument.Reference.Collection(DB_COLLECTION_TASKS).GetSnapshotAsync();
                    foreach (DocumentSnapshot taskDocument in taskSnapshot.Documents)
                    {
                        await taskDocument.Reference.DeleteAsync();
                    }
                    await stationDocument.Reference.DeleteAsync();
                }
                await rallyDocument.Reference.DeleteAsync();
            }
        }

        private async void LoadDBRalliesAll(Action<Dictionary<string, Rally>> resultFunction)
        {
            Debug.Log("Load DB rallies all!");
            Dictionary<string, Rally> result = await this.GetDBSnapshot<Rally>(new string[1] { DB_COLLECTION_RALLIES }, new string[0]);
            resultFunction.Invoke(result);
        }

        private async void LoadDBStations(string rallyId, Action<Dictionary<string, Station>> resultFunction)
        {
            Debug.Log("Load DB stations: " + rallyId);
            string[] path = new string[2] { DB_COLLECTION_RALLIES, DB_COLLECTION_STATIONS };
            string[] ids = new string[1] { rallyId };
            Dictionary<string, Station> result = await this.GetDBSnapshot<Station>(path, ids);
            resultFunction.Invoke(result);
        }

        private async void LoadDBTasks(string rallyId, string stationId, Action<Dictionary<string, RallyTask>> resultFunction)
        {
            Debug.Log("Load DB tasks: " + rallyId + " " + stationId);
            string[] path = new string[3] { DB_COLLECTION_RALLIES, DB_COLLECTION_STATIONS, DB_COLLECTION_TASKS};
            string[] ids = new string[2] { rallyId, stationId };
            Dictionary<string, RallyTask> result = await this.GetDBSnapshot<RallyTask>(path, ids);
            resultFunction.Invoke(result);
        }
    }
}
