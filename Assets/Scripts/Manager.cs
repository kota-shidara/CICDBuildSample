using UnityEngine;
using UnityEngine.UI;
public class Manager : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Text versionText;
    
    void Start()
    {
#if UNITY_STANDALONE_WIN
        text.text = "Windows";
#elif UNITY_STANDALONE_OSX
        text.text = "Mac";
#elif UNITY_ANDROID
        text.text = "Android";
#endif

        versionText.text = "3";
    }
}
