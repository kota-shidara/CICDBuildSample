// using System;
// using System.Linq;
// using UnityEngine;
// using UnityEditor;
// using FrameSynthesis.XR;
// using Cysharp.Threading.Tasks;
// using Unity.VisualScripting;
//
// namespace Editor
// {
//     public static class CICDBuild
//     {
//         static string[] ScenePaths => EditorBuildSettings.scenes.Select(scene => scene.path).ToArray();
//
//         #region PrefsKey
//
//         private const string IsWindowsBuildingKey = "IsWindowsBuilding";
//         private const string IsMacBuildingKey = "IsMacBuilding";
//         private const string IsAndroidBuildingKey = "IsAndroidBuilding";
//
//         #endregion
//
//         #region Paths
//
//         private const string RootPath = "Build/";
//
//         private const string ReleasePath = "Release/";
//         private const string DevelopPath = "Develop/";
//
//         //Windows系
//         private const string WindowsPath = "Windows/";
//         private const string WindowsApplicationName = "Memoria.exe";
//
//         //Mac
//         private const string MacPath = "Mac/";
//         private const string MacApplicationName = "Memoria.app";
//
//         //Android
//         private const string AndroidPath = "Android/";
//         private const string QuestPath = "Quest/";
//         private const string PicoPath = "Pico/";
//         private const string AndroidApplicationName = "Memoria.apk";
//
//         #endregion
//
//         [MenuItem("Tools/Build/Release/All")]
//         public static void BuildReleaseAll()
//         {
//             SetAllKeyFalse();
//             SwitchPlatformWindows();
//         }
//
//         #region EditorLoadMethods
//
//         [InitializeOnLoadMethod]
//         private static void CallWindowsBuild()
//         {
//             Debug.Log("CallWindowsBuild");
//             EditorApplication.delayCall += () =>
//             {
//                 Debug.Log("CallWindowsBuild delayCall" + EditorPrefs.GetBool(IsWindowsBuildingKey));
//                 if (!EditorPrefs.GetBool(IsWindowsBuildingKey)) return;
//                 EditorPrefs.SetBool(IsWindowsBuildingKey, false);
//
//                 BuildReleaseWindows(SwitchPlatformMac);
//             };
//         }
//
//         [InitializeOnLoadMethod]
//         private static void CallMacBuild()
//         {
//             Debug.Log("CallMacBuild");
//             EditorApplication.delayCall += () =>
//             {
//                 Debug.Log("CallMacBuild delayCall" + EditorPrefs.GetBool(IsMacBuildingKey));
//                 if (!EditorPrefs.GetBool(IsMacBuildingKey)) return;
//                 EditorPrefs.SetBool(IsMacBuildingKey, false);
//
//                 BuildReleaseMac(SwitchPlatformAndroid);
//             };
//         }
//
//         [InitializeOnLoadMethod]
//         private static void CallAndroidBuild()
//         {
//             Debug.Log("CallAndroidBuild");
//             EditorApplication.delayCall += () =>
//             {
//                 Debug.Log("CallAndroidBuild delayCall" + EditorPrefs.GetBool(IsAndroidBuildingKey));
//                 if (!EditorPrefs.GetBool(IsAndroidBuildingKey)) return;
//                 EditorPrefs.SetBool(IsAndroidBuildingKey, false);
//
//                 BuildReleaseAndroid();
//             };
//         }
//
//         #endregion
//
//         private static void SetAllKeyFalse()
//         {
//             EditorPrefs.SetBool(IsWindowsBuildingKey, false);
//             EditorPrefs.SetBool(IsMacBuildingKey, false);
//             EditorPrefs.SetBool(IsAndroidBuildingKey, false);
//         }
//
//         #region SwitchPlatform
//         private static void SwitchPlatformWindows()
//         {
//             SwitchPlatform(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64, IsWindowsBuildingKey);
//         }
//
//         private static void SwitchPlatformMac()
//         {
//             SwitchPlatform(BuildTargetGroup.Standalone, BuildTarget.StandaloneOSX, IsMacBuildingKey);
//         }
//
//         private static void SwitchPlatformAndroid()
//         {
//             SwitchPlatform(BuildTargetGroup.Android, BuildTarget.Android, IsAndroidBuildingKey);
//         }
//         
//         private static void SwitchPlatform(BuildTargetGroup targetGroup, BuildTarget target, string prefKey)
//         {
//             EditorUserBuildSettings.SwitchActiveBuildTarget(targetGroup, target);
//
//             EditorApplication.delayCall += () =>
//             {
//                 Debug.Log("SwitchPlatform delayCall" + nameof(prefKey) + ": " + EditorPrefs.GetBool(prefKey));
//                 EditorPrefs.SetBool(prefKey, true);
//             };
//         }
//         
//         #endregion
//
//         #region  Build
//         private static void BuildReleaseWindows(Action onComplete = null)
//         {
//             BuildRelease(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64, WindowsPath,
//                 WindowsApplicationName, true, onComplete);
//         }
//
//         private static void BuildReleaseMac(Action onComplete = null)
//         {
//             BuildRelease(BuildTargetGroup.Standalone, BuildTarget.StandaloneOSX, MacPath, 
//                 MacApplicationName, false, onComplete);
//         }
//
//         private static void BuildReleaseAndroid(Action onComplete = null)
//         {
//             BuildRelease(BuildTargetGroup.Android, BuildTarget.Android, AndroidPath,
//                 AndroidApplicationName, true, onComplete);
//         }
//
//         private static void BuildRelease(BuildTargetGroup targetGroup, BuildTarget target, string outputPath,
//             string applicationName, bool enablePlugin, Action onComplete = null)
//         {
//             Debug.Log($"BuildRelease {target}");
//             if (enablePlugin)
//             {
//                 XRPluginManagementSettings.EnablePlugin(targetGroup, XRPluginManagementSettings.Plugin.OpenXR);
//             }
//             else
//             {
//                 XRPluginManagementSettings.DisablePlugin(targetGroup, XRPluginManagementSettings.Plugin.OpenXR);
//             }
//
//             Debug.Log($"BuildRelease {target} {(enablePlugin ? "EnablePlugin" : "DisablePlugin")}");
//             string buildResultPath = RootPath + ReleasePath + outputPath;
//             BuildPipeline.BuildPlayer(ScenePaths,
//                 buildResultPath + applicationName,
//                 target,
//                 BuildOptions.None);
//
//             onComplete?.Invoke();
//         }
//         
//         #endregion
//     }
// }