using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constructor
{
    internal class ObjectsHandler : MonoBehaviour
    {
        [SerializeField] private List<Transform> objects = new List<Transform>();
        private List<Vector3> objPositions = new List<Vector3>();
        private List<Quaternion> objRotations = new List<Quaternion>();
        private List<Rigidbody2D> objRigidbodies = new List<Rigidbody2D>();

        internal void Init()
        {
            this.objPositions.Clear();
            this.objRotations.Clear();
            this.objRigidbodies.Clear();
            for (int i = 0; i < objects.Count; ++i)
            {
                this.objPositions.Add(objects[i].position);
                this.objRotations.Add(objects[i].rotation);
                this.objRigidbodies.Add(objects[i].GetComponent<Rigidbody2D>());
            }
            ResetObjects();
        }
        
        internal void ResetObjects()
        {
            for (int i = 0; i < objects.Count; ++i)
            {
                this.objects[i].position = this.objPositions[i];
                this.objects[i].rotation = this.objRotations[i];
                //this.objRigidbodies[i].angularDrag = 0f;
                this.objRigidbodies[i].angularVelocity = 0f;
                this.objRigidbodies[i].velocity = Vector2.zero;

            }
        }
    }
}
