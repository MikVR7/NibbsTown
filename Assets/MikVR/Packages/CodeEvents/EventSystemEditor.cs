using UnityEngine;
//using UnityEditor;

namespace CodeEvents
{
    //internal class EventSystemEditor : EditorWindow
    //{
    //    // Add a menu item named "My Window" to the Window menu
    //    [MenuItem("Tools/EventSystem/EventsOverview")]
    //    public static void ShowWindow()
    //    {
    //        //// Show the window
    //        //GetWindow<EventSystemEditor>().Show();
    //    }

    //    //void OnGUI()
    //    //{
    //    //    // Add a header line
    //    //    EditorGUILayout.LabelField("Header Line", EditorStyles.boldLabel);

    //    //    // Add a function to create quads
    //    //    CreateQuad("Quad 1", "Quad 2", new Vector2(10, 10));

    //    //    //Debug.Log("COUNT: " + EventSystemController.VarOut_EventSystems.Count);
    //    //}

    //    //private Vector2 offset = Vector2.zero;
    //    //void CreateQuad(string label1, string label2, Vector2 position)
    //    //{
    //    //    Event currentEvent = Event.current;

    //    //    // Check if the mouse button is pressed on the quad
    //    //    if (currentEvent.type == EventType.MouseDown && currentEvent.button == 0 &&
    //    //        currentEvent.mousePosition.x > position.x && currentEvent.mousePosition.x < position.x + 100 &&
    //    //        currentEvent.mousePosition.y > position.y && currentEvent.mousePosition.y < position.y + 50)
    //    //    {
    //    //        // Update the offset between the mouse position and the top-left corner of the quad
    //    //        offset = GUIUtility.GUIToScreenPoint(currentEvent.mousePosition) - position;
    //    //        Debug.Log("OFFSET: " + offset);
    //    //        // Consume the event to prevent it from propagating
    //    //        currentEvent.Use();
    //    //    }

    //    //    EditorGUI.BeginChangeCheck();

    //    //    //// Create a horizontal group for the quad
    //    //    //GUILayout.BeginHorizontal();

    //    //    //// Create a horizontal group for the quad
    //    //    GUILayout.BeginHorizontal();

    //    //    GUI.Box(new Rect(position.x, position.y, 100, 50), "test");

    //    //    GUI.Label(new Rect(position.x + 10, position.y + 10, 80, 30), "Quad Label");

    //    //    // Add two labels to the quad
    //    //    GUILayout.Label(label1);
    //    //    GUILayout.Label(label2);

    //    //    // End the horizontal group
    //    //    GUILayout.EndHorizontal();

    //    //    if (EditorGUI.EndChangeCheck())
    //    //    {
    //    //        if (currentEvent.type == EventType.MouseDrag && currentEvent.button == 0)
    //    //        {
    //    //            position = GUIUtility.GUIToScreenPoint(Event.current.mousePosition) - offset;
    //    //            Debug.Log("POSITION: " + position);
    //    //        }
    //    //    }
    //    //}
    //}
}
