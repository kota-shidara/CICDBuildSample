using UnityEditor;
using UnityEngine;

public static class DefineSymbolsEditor
{
    private const string PICO_PLATFORM_DEFINE = "PICO_PLATFORM";

    public static void SetPicoOn()
    {
        SetDefineSymbols(true);
    }

    public static void SetPicoOff()
    {
        SetDefineSymbols(false);
    }

    public static void SimpleDebug()
    {
        Debug.Log("====Just a simple debug====");
    }

    private static void SetDefineSymbols(bool isPicoOn)
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