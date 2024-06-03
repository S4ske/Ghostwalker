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
    [SerializeField] private PlayerData playerData;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() && other.isTrigger)
            helpMessage.text = collectableType == CollectableType.Weapon ? "E - Pick Up" : "E - Drink";
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Player>() && other.isTrigger)
        {
            var player = other.GetComponent<Player>();
            if (player.pressedE)
            {
                player.pressedE = false;
                player.currentECd = 0;
                switch (collectableType)
                {
                    case CollectableType.Weapon:
                        other.GetComponent<Inventory>().AddWeapon(weapon);
                        PlayerData.Instance.weapons.Add(weapon.name);
                        Destroy(gameObject);
                        break;
                    case CollectableType.ArmorPotion:
                        other.GetComponent<Player>().GetArmor(3);
                        Destroy(gameObject);
                        break;
                    case CollectableType.ManaPotion:
                        other.GetComponent<Player>().GetMana(50);
                        Destroy(gameObject);
                        break;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player>() && other.isTrigger)
            helpMessage.text = string.Empty;
    }
}
