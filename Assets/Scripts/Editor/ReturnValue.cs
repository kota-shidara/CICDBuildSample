using System.IO;
using System.Text;
using UnityEditor;

public class ReturnValue
{
    private static void WriteStringsToFile(string[] data, string filePath)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var line in data)
        {
            sb.AppendLine(line);
        }
        File.WriteAllText(filePath, sb.ToString());
    }

    [MenuItem("Tools/Test")]
    public static void CreateOutput()
    {
        string[] outputs = new string[]
        {
            "VARIABLE1=value1",
            "VARIABLE2=value22222",
            "VARIABLE3=value3e"
        };
        WriteStringsToFile(outputs, "Build/test.txt");
    }
}