using System.IO;
using System.Text;
using UnityEditor;

public class ReturnValue
{
    
    public static void CreateOutput()
    {
        string[] outputs = new string[]
        {
            "VARIABLE1=value1",
            "VARIABLE2=value2",
            "VARIABLE3=value3"
        };
        WriteStringsToFile(outputs, "Build/test.txt");
    }
    
    private static void WriteStringsToFile(string[] data, string filePath)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var line in data)
        {
            sb.AppendLine(line);
        }
        File.WriteAllText(filePath, sb.ToString());
    }
}