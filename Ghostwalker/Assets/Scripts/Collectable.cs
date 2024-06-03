using UnityEngine;
using UnityEngine.UI;

enum CollectableType
{
    Weapon,
    ArmorPotion,
    ManaPotion
}

public class Collectable : MonoBehaviour
{
    [SerializeField] private CollectableType collectableType;
    [SerializeField] private Text helpMessage;
    [SerializeField] private GameObject weapon;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() && other.isTrigger)
            helpMessage.text = collectableType == CollectableType.Weapon ? "E - Pick Up" : "E - Drink";
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Player>() && other.isTrigger && Input.GetKey(KeyCode.E))
        {
            switch (collectableType)
            {
                case CollectableType.Weapon:
                    var collect = other.GetComponent<Inventory>().AddWeapon(weapon);
                    if (collect)
                        Destroy(gameObject);
                    break;
                case CollectableType.ArmorPotion:
                    other.GetComponent<Player>().GetArmor(3);
                    Destroy(gameObject);
                    break;
                case CollectableType.ManaPotion:
                    other.GetComponent<Player>().GetMana(25);
                    Destroy(gameObject);
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player>() && other.isTrigger)
            helpMessage.text = string.Empty;
    }
}
