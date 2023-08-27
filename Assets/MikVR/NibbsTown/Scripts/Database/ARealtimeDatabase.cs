using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using UnityEngine;
using Firebase.Firestore;

namespace NibbsTown
{
    internal abstract class ARealtimeDatabase
    {
        protected FirebaseFirestore firebaseFirestore = null;

        internal virtual void Init(FirebaseFirestore firebaseFirestore) {
            this.firebaseFirestore = firebaseFirestore;
    	} 
    //    ...TODO: exists...
    //// Get the current Task
    //DocumentSnapshot taskSnapshot = await taskRef.GetSnapshotAsync();

        protected async Task<Dictionary<string, T>> GetDBSnapshot<T>(string[] path, string[] ids)
        {
            Dictionary<string, T> results = new Dictionary<string, T>();

            try
            {
                QuerySnapshot querySnapshot = await GetQuerySnapshot(path, ids);

                if (querySnapshot != null)
                {
                    foreach (DocumentSnapshot document in querySnapshot.Documents)
                    {
                        if (document.Exists)
                        {
                            results.Add(document.Id, document.ConvertTo<T>());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to get DB snapshot: {e}");
            }

            return results;
        }

        private async Task<QuerySnapshot> GetQuerySnapshot(string[] path, string[] ids)
        {
            DocumentReference docRef = null;
            QuerySnapshot querySnapshot = null;
            for (int i = 0; i < path.Length; i++)
            {
                if (docRef == null)
                {
                    if (ids.Length > i)
                    {
                        docRef = firebaseFirestore.Collection(path[i]).Document(ids[i]);
                    }
                    else
                    {
                        querySnapshot = await firebaseFirestore.Collection(path[i]).GetSnapshotAsync();
                    }
                }
                else
                {
                    if (ids.Length > i)
                    {
                        docRef = docRef.Collection(path[i]).Document(ids[i]);

                    }
                    else
                    {
                        querySnapshot = await docRef.Collection(path[i]).GetSnapshotAsync();
                    }
                }
            }
            return querySnapshot;
        }
    }
}
