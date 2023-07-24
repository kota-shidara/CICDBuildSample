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
    private const string WindowsAppName = "GitHubActionsBuild.exe";
    
    //Mac
    private const string MacPath = "Mac/";
    private const string MacAppName = "GitHubActionsBuild.app";
    
    //Quest
    private const string QuestPath = "Quest/";
    private const string QuestAppName = "GitHubActionsBuild.apk";

    #endregion

    
    public static void BuildReleaseWindows()
    {
        BuildRelease(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64, WindowsPath, 
            WindowsAppName, true);
    }
    
    public static void BuildReleaseMac()
    {
        BuildRelease(BuildTargetGroup.Standalone, BuildTarget.StandaloneOSX, MacPath, 
            MacAppName, false);
    }
    
    public static void BuildReleaseQuest()
    {
        BuildRelease(BuildTargetGroup.Android, BuildTarget.Android, QuestPath, 
            QuestAppName, true);
    }

    private static void BuildRelease(BuildTargetGroup targetGroup, BuildTarget target, string outputPath,
        string appName, bool enablePlugin)
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
            buildResultPath + appName,
            target,
            BuildOptions.None);
    }
}