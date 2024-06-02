using UnityEngine;

public class TextSlider : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1f);
    }
}
