using UnityEngine;

public class NativeCount : MonoBehaviour
{

    void Update()
    {
        var foundNative = Object.FindObjectsOfType<Unit>();
        int nativeCount = foundNative.Length;
        StatCon.totalNative = nativeCount;
    }
}
