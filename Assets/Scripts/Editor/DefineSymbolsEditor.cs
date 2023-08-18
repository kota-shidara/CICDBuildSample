using UnityEditor;
using UnityEngine;

namespace Flamers.Editor.Tools
{
    internal class DefineSymbolsEditor : EditorWindow
    {
        private const string PICO_PLATFORM_DEFINE = "PICO_PLATFORM";

        public void SetPicoOn()
        {
            SetDefineSymbols(true);
        }

        public void SetPicoOff()
        {
            SetDefineSymbols(false);
        }

        private void SetDefineSymbols(bool isPicoOn)
        {
            var symbols =
                PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);

            if (isPicoOn)
            {
                if (!symbols.Contains(PICO_PLATFORM_DEFINE))
                {
                    symbols += $";{PICO_PLATFORM_DEFINE}";
                }
            }
            else
            {
                symbols = symbols.Replace($"{PICO_PLATFORM_DEFINE};", "")
                    .Replace(PICO_PLATFORM_DEFINE, "");
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, symbols);
        }
    }
}