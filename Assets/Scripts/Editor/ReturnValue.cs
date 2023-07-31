using UnityEngine;

public class ReturnValue
{
    private static void ReturnValueMethod()
    {
        Debug.Log("ReturnValueMethodが呼ばれた");
        Debug.Log("echo \"test=hello\" >> $GITHUB_OUTPUT");
    }
}