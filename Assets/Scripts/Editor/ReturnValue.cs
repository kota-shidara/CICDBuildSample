using UnityEngine;

public class ReturnValue
{
    private static void ReturnValueMethod()
    {
        Debug.Log("ReturnValueMethodが呼ばれた");
        Debug.Log("\"test=hello\" >> \"$GITHUB_OUTPUT\"");
    }
}