using TMPro;
using UnityEngine;

public class StatCon : MonoBehaviour
{
    public TextMeshProUGUI Stone,Food,Native;
    public static int totalStone = 0;
    public static int totalFood = 0;
    public static int totalNative = 0;

    void Update()
    {
        Stone.text = totalStone + " ";
        Food.text = totalFood + " ";
        Native.text = totalNative + " ";
    }
}
