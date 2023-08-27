using UnityEditor;

public class ChangeNamingScheme
{
    [InitializeOnLoadMethod]
    private static void ChangeScheme()
    {
        EditorSettings.gameObjectNamingScheme = EditorSettings.NamingScheme.Underscore;
        EditorSettings.assetNamingUsesSpace = false;
    }
}
