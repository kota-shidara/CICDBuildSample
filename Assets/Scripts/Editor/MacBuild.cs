using System;
using System.Linq;
using UnityEngine;
using UnityEditor;
using FrameSynthesis.XR;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;

namespace Editor
{
    public static class MacBuild
    {
        static string[] ScenePaths => EditorBuildSettings.scenes.Select(scene => scene.path).ToArray();

        #region Paths

        private const string RootPath = "Build/";

        private const string ReleasePath = "Release/";
        private const string DevelopPath = "Develop/";

        //Mac
        private const string MacPath = "Mac/";
        private const string MacApplicationName = "Memoria.app";

        #endregion

        [MenuItem("Tools/Build/Release/Mac")]
        private static void BuildReleaseMac()
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
}