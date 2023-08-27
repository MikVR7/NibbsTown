using Firebase.Database;
using System.Collections.Generic;
using System;
using UnityEngine;
using Newtonsoft.Json;
using Firebase.Firestore;

namespace NibbsTown
{
    internal class DatabaseMultiplayer : ARealtimeDatabase
    {
        private static DatabaseMultiplayer instance;
        internal static EventInOut_LoadAllPlayers EventInOut_LoadAllPlayers = new EventInOut_LoadAllPlayers();
        internal static EventIn_AddPlayer EventIn_AddPlayer = new EventIn_AddPlayer();
        internal static EventIn_RemovePlayer EventIn_RemovePlayer = new EventIn_RemovePlayer();

        internal static readonly string PATH_MULTIPLAYER = "multiplayer/";
        internal static readonly string PATH_GROUPS = "groups/";
        internal static readonly string PATH_PLAYERS = "players/";

        internal static string VarOut_GetDBKey()
        {
            // Generate a unique key for the data
            return instance.referencePlayers.Push().Key;
        }

        private DatabaseReference referencePlayers = null;

        internal override void Init(FirebaseFirestore firebaseFirestore) {
            base.Init(firebaseFirestore);
            instance = this;
            //this.referencePlayers = firebaseDatabase.GetReference(PATH_MULTIPLAYER + PATH_GROUPS + PATH_PLAYERS);

            EventInOut_LoadAllPlayers.AddListenerSingle(LoadAllPlayers);
            EventIn_AddPlayer.AddListenerSingle(AddPlayer);
            EventIn_RemovePlayer.AddListenerSingle(RemovePlayer);
        }

        private void LoadAllPlayers(Action<Dictionary<string, Group>> resultFunction)
        {
            //_ = LoadDbData(PATH_MULTIPLAYER + PATH_GROUPS, resultFunction);
        }

        private void AddPlayer(Player player)
        {
            string jsonString = JsonConvert.SerializeObject(player);
            this.referencePlayers.Child(player.Key).SetRawJsonValueAsync(jsonString);
        }

        private void RemovePlayer(string key)
        {
            // Get a DatabaseReference
            //DatabaseReference reference = firebaseDatabase.GetReference(PATH_MULTIPLAYER + PATH_GROUPS + PATH_PLAYERS);
            // Remove the data at the specified key
            //reference.Child(key).RemoveValueAsync();
        }
    }
}
