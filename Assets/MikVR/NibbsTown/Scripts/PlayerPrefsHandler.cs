using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace NibbsTown
{
    internal class PlayerPrefsHandler
    {
        private static readonly string PP_PREFIX = "NibbsTown/";
        private static readonly string PP_STARTED_RALLIES = "StartedRallies/";

        internal static EventIn_SetFinishedRallyStation EventIn_SetFinishedRallyStation = new EventIn_SetFinishedRallyStation();
        internal static EventIn_DeleteFinishedRallyStations EventIn_DeleteFinishedRallyStations = new EventIn_DeleteFinishedRallyStations();

        internal void Init()
        {
            EventIn_SetFinishedRallyStation.AddListenerSingle(SetFinishedRallyStation);
            EventIn_DeleteFinishedRallyStations.AddListenerSingle(DeleteFinishedRallyStations);
        }

        private void SetFinishedRallyStation(string rallyKey, string stationKey)
        {
            string path = PP_PREFIX + PP_STARTED_RALLIES + rallyKey;
            List<string> stationKeys = new List<string>();
            if(PlayerPrefs.HasKey(path))
            {
                stationKeys = JsonConvert.DeserializeObject<List<string>>(PlayerPrefs.GetString(path));
            }
            if(!stationKeys.Contains(stationKey))
            {
                stationKeys.Add(stationKey);
            }
            PlayerPrefs.SetString(path, JsonConvert.SerializeObject(stationKeys));
        }

        internal static List<string> VarOut_GetRallyStationKeys(string rallyKey)
        {
            string path = PP_PREFIX + PP_STARTED_RALLIES + rallyKey;
            List<string> stationKeys = new List<string>();
            if (PlayerPrefs.HasKey(path))
            {
                stationKeys = JsonConvert.DeserializeObject<List<string>>(PlayerPrefs.GetString(path));
            }
            return stationKeys;
        }

        private void DeleteFinishedRallyStations(string rallyKey)
        {
            string path = PP_PREFIX + PP_STARTED_RALLIES + rallyKey;
            if (PlayerPrefs.HasKey(path))
            {
                PlayerPrefs.DeleteKey(path);
            }
        }
    }
}
