//using System.Collections.Generic;

//namespace NibbsTown
//{
//    internal class MultiplayerHandler
//    {
//        private static MultiplayerHandler instance = null;
//        private static readonly string NAME_BASE_GROUP = "base_group";

//        private Dictionary<string, Group> groups = new Dictionary<string, Group>();
//        private Player self = new Player();
//        internal static Player VarOut_Self() { return instance.self; }

//        internal void Init() {
//            instance = this;
//            // create base group
//            this.groups.Clear();
//            this.groups.Add(NAME_BASE_GROUP, new Group());

//            PanelLogin.EventOut_UsernameChanged.AddListenerSingle(UserLoggedIn);
//        }



//        private void UserLoggedIn(string username)
//        {
//            self.Name = username;
//            self.DBKey = "";// DatabaseMultiplayer.VarOut_GetDBKey();
//            self.GPSPosition = GPSHandler.VarOut_LastPosition;
//            DatabaseMultiplayer.EventInOut_LoadAllPlayers.Invoke(OnAllPlayersLoaded);
//            MapObjectsHandler.EventIn_AddMapObjectCharacter.Invoke(self.GPSPosition, self.Name);
//            GPSHandler.EventOut_OnNewGPSCoordinates.AddListenerSingle(OnNewGPSCoordinates);
//        }

//        private void OnAllPlayersLoaded(Dictionary<string, Group> groups)
//        {
//            this.groups = groups;
//            // add players listener here!
            
//            // add player
//            DatabaseMultiplayer.EventIn_AddPlayer.Invoke(self);
            
//        }

//        private void OnNewGPSCoordinates(GPSPosition gpsPosition)
//        {
//            this.self.GPSPosition = gpsPosition;
//            MapObjectsHandler.EventIn_SetMapObjectPosition.Invoke(gpsPosition, self.Name);
//        }
//    }
//}
