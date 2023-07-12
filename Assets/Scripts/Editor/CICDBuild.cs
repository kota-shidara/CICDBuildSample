using System;
using System.Linq;
using UnityEngine;
using UnityEditor;
using FrameSynthesis.XR;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;

namespace Editor
{
    public static class CICDBuild
    {
        static string[] ScenePaths => EditorBuildSettings.scenes.Select(scene => scene.path).ToArray();

        #region Paths

        private const string RootPath = "Build/";

        private const string ReleasePath = "Release/";
        private const string DevelopPath = "Develop/";

        //Windows系
        private const string WindowsPath = "Windows/";
        private const string WindowsApplicationName = "Memoria.exe";
        
        //Mac
        private const string MacPath = "Mac/";
        private const string MacApplicationName = "Memoria.app";
        
        //Android
        private const string AndroidPath = "Android/";
        private const string QuestPath = "Quest/";
        private const string PicoPath = "Pico/";
        private const string AndroidApplicationName = "Memoria.apk";

        #endregion
        
        [MenuItem("Tools/Build/Release/All")]
        public static void BuildReleaseAll()
        {
            BuildReleaseWindows(() =>
            {
                BuildReleaseMac();
            });
        }
        
        private static void BuildReleaseWindows(Action onComplete = null)
        {
            Debug.Log("Before Switching");
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);
            Debug.Log("After Switching");
            
            EditorApplication.delayCall += () =>
            {
                Debug.Log("Soon After DelayCall");
                EditorApplication.update += BuildAfterCompilation;

                void BuildAfterCompilation()
                {
                    if (EditorApplication.isCompiling)
                    {
                        Debug.Log("Compiling");
                        return;
                    }

                    Debug.Log("Compile Finished");
                    EditorApplication.update -= BuildAfterCompilation;

                    XRPluginManagementSettings.EnablePlugin(BuildTargetGroup.Standalone,
                        XRPluginManagementSettings.Plugin.OpenXR);

                    string buildResultPath = RootPath + ReleasePath + WindowsPath;
                    BuildPipeline.BuildPlayer(ScenePaths,
                        buildResultPath + WindowsApplicationName,
                        BuildTarget.StandaloneWindows64,
                        BuildOptions.None);

                    onComplete?.Invoke();
                }
            };
        }

        private static void BuildReleaseMac(Action onComplete = null)
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneOSX);

            EditorApplication.delayCall += () =>
            {
                EditorApplication.update += BuildAfterCompilation;

                void BuildAfterCompilation()
                {
                    if (EditorApplication.isCompiling)
                    {
                        return;
                    }

                    EditorApplication.update -= BuildAfterCompilation;

                    XRPluginManagementSettings.DisablePlugin(BuildTargetGroup.Standalone,
                        XRPluginManagementSettings.Plugin.OpenXR);

                    string buildResultPath = RootPath + ReleasePath + MacPath;
                    BuildPipeline.BuildPlayer(ScenePaths,
                        buildResultPath + MacApplicationName,
                        BuildTarget.StandaloneOSX,
                        BuildOptions.None);

                    onComplete?.Invoke();
                }
            };
        }


        private static void BuildReleaseAndroid(Action onComplete = null)
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);

            EditorApplication.delayCall += async () =>
            {
                await UniTask.WaitUntil(() => !EditorApplication.isCompiling);
                
                XRPluginManagementSettings.EnablePlugin(BuildTargetGroup.Android,
                    XRPluginManagementSettings.Plugin.OpenXR);

                string buildResultPath = RootPath + ReleasePath + AndroidPath + QuestPath;
                BuildPipeline.BuildPlayer(ScenePaths,
                    buildResultPath + AndroidApplicationName,
                    BuildTarget.Android,
                    BuildOptions.None);

                onComplete?.Invoke();
            };
        }
    }
}

