using UnityEngine;
using UnityEngine.UI;

public class TextFotCoin : MonoBehaviour
{
    public static int Coin;
    private Text text;
    
    void Start()
    {
        text = GetComponent<Text>();
    }
    
    void Update()
    {
        text.text = $"COINS: {Coin}";
    }
}
