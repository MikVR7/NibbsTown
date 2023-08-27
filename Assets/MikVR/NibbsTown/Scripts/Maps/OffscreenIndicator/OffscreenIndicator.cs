using System;
using UnityEngine;

namespace NibbsTown
{
    internal class OffscreenIndicator : MonoBehaviour
    {
        private RectTransform myRectTransform = null;
        private AMapObject mapObject = null; // The off-screen object
        private RectTransform rtParent = null;
        [SerializeField] private Vector2 offset = Vector2.zero;
        private bool displayArrow = false;

        private static Vector2 canvasSize = Vector2.zero;

        internal void Init(AMapObject mapObject) {
            this.mapObject = mapObject;
            this.myRectTransform = this.GetComponent<RectTransform>();
            this.rtParent = this.myRectTransform.parent.GetComponent<RectTransform>();
            NibbsTownMainMenu.EventOut_OnUpdateAt50.AddListener(OnUpdateAt50);
            this.gameObject.SetActive(false);

            if (canvasSize == Vector2.zero)
            {
                Vector3[] corners = new Vector3[4];
                rtParent.GetWorldCorners(corners);
                Vector3 bottomLeft = corners[0];
                Vector3 topRight = corners[2];
                float width = topRight.x - bottomLeft.x;
                float height = topRight.y - bottomLeft.y;
                // Calculate the position on the edge of the canvas based on the bearing
                canvasSize = new Vector2(width, height) * 100f;
            }
        }

        internal void DestroyObject()
        {
            MapsHandler.EventOut_OnMapChangedPosition.RemoveListener(OnMapChangedPosition);
            NibbsTownMainMenu.EventOut_OnUpdateAt50.AddListener(OnUpdateAt50);
            Destroy(this.gameObject);
        }

        private void OnUpdateAt50()
        {
            if(this == null || this.gameObject == null) { return; }
            return;
            this.displayArrow = !this.mapObject.gameObject.activeSelf;
            
            if (displayArrow && !this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(true);
                MapsHandler.EventOut_OnMapChangedPosition.AddListener(OnMapChangedPosition);
                OnMapChangedPosition();
            }
            else if ((!displayArrow) && this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(false);
                MapsHandler.EventOut_OnMapChangedPosition.RemoveListener(OnMapChangedPosition);
            }
        }

        private double objectLat = 0;
        private double objectLon = 0;
        private GPSPosition gpsPosCamera = GPSPosition.Zero;
        private double bearing = 0d;
        private double angleRad = 0d; // Subtract 90 degrees from the bearing before converting to radians
        private Vector2 positionOnUnitCircle = Vector2.zero;
        private float aspectRatio = canvasSize.x / canvasSize.y;
        private float x = 0f;
        private float y = 0f;
        private float edgeFactor = 0f;
        private Vector2 positionOnEdge = Vector2.zero;
        private Vector2 positionOnCanvas = Vector2.zero;

        private void OnMapChangedPosition()
        {
            this.mapObject.VarOut_OnlineMapsMarker3D.GetPosition(out objectLon, out objectLat);

            gpsPosCamera = MapsHandler.VarOut_GetMapPosition();

            bearing = CalculateBearing(gpsPosCamera.Latitude, gpsPosCamera.Longitude, objectLat, objectLon);
            angleRad = -(bearing - 90) * Mathf.Deg2Rad; // Subtract 90 degrees from the bearing before converting to radians
            positionOnUnitCircle = new Vector2(Mathf.Cos((float)angleRad), Mathf.Sin((float)angleRad)) + offset;

            //////// Calculate the intersection of the line from the center of the canvas to the unit circle with the edge of the canvas
            aspectRatio = canvasSize.x / canvasSize.y;
            x = aspectRatio * positionOnUnitCircle.x;
            y = positionOnUnitCircle.y;
            edgeFactor = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));
            positionOnEdge = new Vector2(x / edgeFactor, y / edgeFactor);

            //////// Convert the position on the edge (which ranges from -1 to 1) to a position on the canvas
            positionOnCanvas = ((positionOnEdge + Vector2.one) * canvasSize) - canvasSize;
            this.myRectTransform.localEulerAngles = new Vector3(0, 0, -(float)bearing);
            this.myRectTransform.anchoredPosition = positionOnCanvas;
        }

        private double dLon = 0d;
        private double yPos = 0d;
        private double xPos = 0d;
        private double brng = 0d;
        private double CalculateBearing(double lat1, double lon1, double lat2, double lon2)
        {
            // Convert latitude and longitude values to radians
            lat1 = lat1 * Math.PI / 180f;
            lon1 = lon1 * Math.PI / 180f;
            lat2 = lat2 * Math.PI / 180f;
            lon2 = lon2 * Math.PI / 180f;
            dLon = (lon2 - lon1);
            yPos = Math.Sin(dLon) * Math.Cos(lat2);
            xPos = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(dLon);
            brng = ((Math.Atan2(yPos, xPos) * Mathf.Rad2Deg) + 360) % 360;
            return brng;
        }
    }
}
