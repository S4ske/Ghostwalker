using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Text text;
    [SerializeField] private SpriteRenderer image;
    private int hotWeapon;
    [SerializeField] private Sprite emptyImage;

    private void Update()
    {
        if (inventory.weapons.Count == 0 && hotWeapon != -1)
        {
            text.text = "Empty";
            image.sprite = emptyImage;
            hotWeapon = -1;
        }
        else if (inventory.weapons.Count > 0 && hotWeapon != inventory.hotWeapon)
        {
            hotWeapon = inventory.hotWeapon;
            text.text = inventory.weapons[inventory.hotWeapon].GetComponent<Weapon>().weaponName;
            image.sprite = inventory.weapons[inventory.hotWeapon].GetComponent<SpriteRenderer>().sprite;
        }
    }
}
