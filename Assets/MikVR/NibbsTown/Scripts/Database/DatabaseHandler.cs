using Firebase;
using Firebase.Database;
using Firebase.Storage;
using Firebase.Firestore;
using System;
using UnityEngine;
using Firebase.Extensions;

namespace NibbsTown
{
    internal class DatabaseHandler
    {
        private static readonly string API_KEY = "AIzaSyAZtDvKrSEU7jCS1bzaGEalRX-NGELRAxQ";
        private static readonly string PROJECT_ID = "townrally-userbase"; // You can find this in your Firebase project settings
        private static readonly string DATABASE_URL = "https://townrally-userbase-default-rtdb.europe-west1.firebasedatabase.app/";
        private static readonly string APP_ID = "1:87340128502:android:c4096aaf2190a8b103f4d7";
        private static readonly string STORAGE_BUCKET = "townrally-userbase.appspot.com";// "com.Tokele.TownRallyUI";
        
        private DatabaseStorage databaseStorage = new DatabaseStorage();
        private DatabaseRallies databaseRallies = new DatabaseRallies();
        private DatabaseMultiplayer databaseMultiplayer = new DatabaseMultiplayer();

        internal void Init()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                if (task.Exception != null)
                {
                    Debug.LogError($"Failed to initialize Firebase with {task.Exception}");
                    return;
                }

                Uri uri = new Uri(DATABASE_URL);
                FirebaseApp firebaseApp = FirebaseApp.Create(new AppOptions()
                {
                    ApiKey = API_KEY,
                    AppId = APP_ID,
                    ProjectId = PROJECT_ID,
                    DatabaseUrl = uri,
                    StorageBucket = STORAGE_BUCKET,
                });

                FirebaseDatabase firebaseDatabase = FirebaseDatabase.GetInstance(firebaseApp);
                FirebaseFirestore firebaseFirestore = FirebaseFirestore.GetInstance(firebaseApp);
                databaseStorage.Init(FirebaseStorage.GetInstance(firebaseApp));
                databaseRallies.Init(firebaseFirestore);
                databaseMultiplayer.Init(firebaseFirestore);
            });
        }
    }
}
 