using UnityEngine;
using UnityEngine.UI;

enum CollectableType
{
    Weapon,
    Potion
}

public class Collectable : MonoBehaviour
{
    [SerializeField] private CollectableType collectableType;
    [SerializeField] private Text helpMessage;
    [SerializeField] private GameObject weapon;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() && other.isTrigger)
            helpMessage.text = "E - pick up";
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Player>() && other.isTrigger && Input.GetKey(KeyCode.E))
        {
            if (collectableType == CollectableType.Weapon)
            {
                other.GetComponent<Inventory>().AddWeapon(weapon);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player>() && other.isTrigger)
            helpMessage.text = string.Empty;
    }
}
