using UnityEngine;
using UnityEngine.UI;
public class Manager : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Text versionText;
    [SerializeField] private Text processorCount;
    [SerializeField] private Text processorFrequency;
    
    void Start()
    {
#if UNITY_STANDALONE_WIN
        text.text = "Windows";
#elif UNITY_STANDALONE_OSX
        text.text = "Mac";
#elif UNITY_ANDROID
        text.text = "Android";
#endif

#if PICO_PLATFORM
        versionText.text = "PICO";
#else
        versionText.text = "NOT_PICO";
#endif
        
        processorCount.text = SystemInfo.processorCount.ToString();
        processorFrequency.text = SystemInfo.processorFrequency.ToString();
    }
}
