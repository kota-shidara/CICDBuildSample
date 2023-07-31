using UnityEngine;

public class ReturnValue
{
    private static string ReturnValueMethod()
    {
        Debug.Log("ReturnValueMethodが呼ばれた");
        Debug.Log("echo \"test=hello\" >> $GITHUB_OUTPUT");
        return "valuevaluevalue";
    }
}