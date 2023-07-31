using UnityEngine;

public class ReturnValue
{
    private static string ReturnValueMethod()
    {
        Debug.Log("ReturnValueMethodが呼ばれた");
        return "ReturnValueMethod_ReturnValue";
    }
}