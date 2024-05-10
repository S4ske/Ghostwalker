using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<GameObject> weapons;
    private List<IPotion> potions = new ();
    [SerializeField] private int hotWeapon;
    [SerializeField] private int maxWeapons = 2;
    [SerializeField] private int maxPotions = 2;

    private void Update()
    {
        int scroll = Convert.ToInt32(Input.GetAxisRaw("Mouse ScrollWheel") * 10);

        if (weapons.Count > 0 && scroll != 0)
        {
            int newHotWeapon = ((hotWeapon + scroll) % weapons.Count + weapons.Count) % weapons.Count;
            SetHotWeapon(newHotWeapon);
        }
        
        if (weapons.Count > 0 && Input.GetKeyDown(KeyCode.G) && hotWeapon < weapons.Count)
            RemoveWeapon(weapons[hotWeapon]);
        
        if (potions.Count > 0 && Input.GetKeyDown(KeyCode.H))
        {
            potions[0].Drink();
            potions.RemoveAt(0);
        }
    }

    private void SetHotWeapon(int newHotWeapon)
    {
        weapons[hotWeapon].SetActive(false);
        hotWeapon = newHotWeapon;
        weapons[hotWeapon].SetActive(true);
    }

    public void AddWeapon(GameObject weapon)
    {
        if (weapons.Count < maxWeapons)
        {
            GameObject newWeapon = Instantiate(weapon, transform);
            weapons.Add(newWeapon);
            SetHotWeapon(weapons.Count - 1);
        }
        else
        {
            RemoveWeapon(weapons[hotWeapon]);
            AddWeapon(weapon);
        }
    }

    public void RemoveWeapon(GameObject weapon)
    {
        Instantiate(weapon.GetComponent<Weapon>().collectable, transform.position, Quaternion.identity);
        weapons.Remove(weapon);
        Destroy(weapon);
        if (weapons.Count > 0)
        {
            hotWeapon = hotWeapon >= weapons.Count ? weapons.Count - 1 : hotWeapon;
            SetHotWeapon(hotWeapon);
        }
    }

    public void AddPotion(IPotion potion)
    {
        if (maxPotions > potions.Count)
            potions.Add(potion);
    }
}