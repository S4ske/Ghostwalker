using UnityEngine;
using UnityEngine.UI;

public class StartDoor : MonoBehaviour
{
    [SerializeField] private Text text;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            var player = other.GetComponent<Player>();
            if (player.WithMantle)
                text.text = "E - break door";
            else
                text.text = "Locked";
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            var player = other.GetComponent<Player>();
            if (player.WithMantle)
                text.text = "E - break door";
            else
                text.text = "Locked";
            if (player.WithMantle && Input.GetKey(KeyCode.E))
                Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
            text.text = "";
    }
}
