using UnityEditor;
using System.Diagnostics;
using UnityEngine;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

public class ProjectBuilder : EditorWindow
{
    //string arg1 = "";
    //string arg2 = "";
    //string batchFilePath = "C:\\Projects\\UnityProjectBuilder\\BatchScripts\\building_bridge.bat"; // You should specify the path to your batch file here
    StringBuilder output = new StringBuilder();
    Vector2 scrollPosition;

    [MenuItem("Window/BuildProjectInSeparateProject")]
    public static void ShowWindow()
    {
        ProjectBuilder projectBuilder = GetWindow<ProjectBuilder>("ProjectBuilder");
        //projectBuilder.arg1 = Application.dataPath.Replace("/Assets", string.Empty);
        //projectBuilder.arg2 = "NibbsTown";
    }

    void OnGUI()
    {
        //arg1 = EditorGUILayout.TextField("Project path: ", arg1);
        //arg2 = EditorGUILayout.TextField("Build name:", arg2);
        //batchFilePath = EditorGUILayout.TextField("Batch file path", batchFilePath);

        if (GUILayout.Button("Run build"))
        {
            //RunBatchFile(batchFilePath, arg1, arg2);
            BuildExternalProject();
        }

        //if (GUILayout.Button("Open Builds Folder"))
        //{
        //    OpenBuildsFolder(arg1);
        //}

        //if (GUILayout.Button("Clear Output"))
        //{
        //    ClearOutput();
        //}

        // Display the output from the batch file
        EditorGUILayout.LabelField("Output:");
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(100));
        EditorGUILayout.TextArea(output.ToString());
        EditorGUILayout.EndScrollView();
    }

    public void BuildExternalProject()
    {
        UnityEngine.Debug.Log("START BUILDING PROJECT!");
        string assetsPath = Application.dataPath;
        string projectPath = Path.GetDirectoryName(assetsPath);
        string unityVersion = ReadUnityVersion(projectPath);

        string unityPath = "C:\\Program Files\\Unity\\Hub\\Editor\\" + unityVersion + "\\Editor\\Unity.exe"; // Set this to the path to your Unity.exe
        string buildProjectPath = projectPath + "\\Builds"; // Path where the project should be built
        string buildProjectName = "NibbsTown"; // Name of the project to be built

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = unityPath,
            Arguments = $"-quit -batchmode -projectPath \"{projectPath}\" -executeMethod ProjectBuilder.BuildAndroidProject \"{buildProjectPath}\" \"{buildProjectName}\"",
            UseShellExecute = false,
            RedirectStandardOutput = true
        };

        Process process = new Process { StartInfo = startInfo };
        process.Start();

        // Capture the output
        string output = process.StandardOutput.ReadToEnd();

        process.WaitForExit();

        // Log or write the output to a file for debugging purposes
        UnityEngine.Debug.Log(output);
        UnityEngine.Debug.Log("DONE BUILDING!");
    }

    public static string ReadUnityVersion(string projectPath)
    {
        string projectVersionPath = Path.Combine(projectPath, "ProjectSettings", "ProjectVersion.txt");

        if (!File.Exists(projectVersionPath))
        {
            return "File not found";
        }

        string fileContents = File.ReadAllText(projectVersionPath);

        // Regular expression to match the version number.
        Regex regex = new Regex("m_EditorVersion: (.+)");
        Match match = regex.Match(fileContents);

        if (match.Success)
        {
            return match.Groups[1].Value;
        }
        else
        {
            return "Version not found";
        }
    }

    //void RunBatchFile(string filePath, string argument1, string argument2)
    //{
    //    ProcessStartInfo startInfo = new ProcessStartInfo()
    //    {
    //        FileName = filePath,
    //        Arguments = $"{argument1} {argument2}",
    //        UseShellExecute = false,
    //        RedirectStandardOutput = true,
    //        RedirectStandardError = true,
    //        WorkingDirectory = Path.GetDirectoryName(filePath)
    //    };

    //    Process process = new Process()
    //    {
    //        StartInfo = startInfo,
    //        EnableRaisingEvents = true
    //    };

    //    process.OutputDataReceived += (sender, eventArgs) =>
    //    {
    //        if (!string.IsNullOrEmpty(eventArgs.Data))
    //        {
    //            AppendOutput(eventArgs.Data);
    //        }

    //        // Check if the process has exited
    //        if (process.HasExited)
    //        {
    //            AppendOutput("Process exited");
    //        }
    //    };

    //    process.ErrorDataReceived += (sender, eventArgs) =>
    //    {
    //        if (!string.IsNullOrEmpty(eventArgs.Data))
    //        {
    //            AppendOutput(eventArgs.Data);
    //        }

    //        // Check if the process has exited
    //        if (process.HasExited)
    //        {
    //            AppendOutput("Process exited");
    //        }
    //    };

    //    process.Start();

    //    process.BeginOutputReadLine();
    //    process.BeginErrorReadLine();
    //}

    //void AppendOutput(string outputData)
    //{
    //    // Append the new data to the output
    //    output.AppendLine(outputData);

    //    // Schedule the editor window to be repainted
    //    this.Repaint();
    //}

    //void OpenBuildsFolder(string projectPath)
    //{
    //    string buildsFolderPath = Path.Combine(projectPath, "Builds");
    //    if (Directory.Exists(buildsFolderPath))
    //    {
    //        Process.Start(buildsFolderPath);
    //    }
    //    else
    //    {
    //        UnityEngine.Debug.LogError($"Builds folder not found: {buildsFolderPath}");
    //    }
    //}

    //void ClearOutput()
    //{
    //    output.Clear();
    //    Repaint();
    //}
}
