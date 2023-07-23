using System.Linq;
using UnityEngine;
using UnityEditor;
using FrameSynthesis.XR;

public static class Build
{
    static string[] ScenePaths => EditorBuildSettings.scenes.Select(scene => scene.path).ToArray();

    #region Paths

    private const string RootPath = "Build/";

    private const string ReleasePath = "Release/";
    private const string DevelopPath = "Develop/";

    //windows
    private const string WindowsPath = "Windows/";
    private const string WindowsApplicationName = "GitHubActionsBuild.exe";
    
    //Mac
    private const string MacPath = "Mac/";
    private const string MacApplicationName = "GitHubActionsBuild.app";

    #endregion

    
    [MenuItem("Tools/Build/Release/Windows")]
    public static void BuildReleaseWindows()
    {
        BuildRelease(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64, WindowsPath, 
            WindowsApplicationName, true);
    }

    [MenuItem("Tools/Build/Release/Mac")]
    public static void BuildReleaseMac()
    {
        BuildRelease(BuildTargetGroup.Standalone, BuildTarget.StandaloneOSX, MacPath, 
            MacApplicationName, false);
    }

    private static void BuildRelease(BuildTargetGroup targetGroup, BuildTarget target, string outputPath,
        string applicationName, bool enablePlugin)
    {
        Debug.Log($"BuildRelease {target}");
        if (enablePlugin)
        {
            XRPluginManagementSettings.EnablePlugin(targetGroup, XRPluginManagementSettings.Plugin.OpenXR);
        }
        else
        {
            XRPluginManagementSettings.DisablePlugin(targetGroup, XRPluginManagementSettings.Plugin.OpenXR);
        }

        Debug.Log($"BuildRelease {target} {(enablePlugin ? "EnablePlugin" : "DisablePlugin")}");
        string buildResultPath = RootPath + ReleasePath + outputPath;
        BuildPipeline.BuildPlayer(ScenePaths,
            buildResultPath + applicationName,
            target,
            BuildOptions.None);
    }
}