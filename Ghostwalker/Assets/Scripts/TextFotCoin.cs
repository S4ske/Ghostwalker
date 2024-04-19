using UnityEngine;
using UnityEngine.UI;

public class TextFotCoin : MonoBehaviour
{
    public static int Coin;
    private Text text;
    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = text.ToString();
    }
}
