using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class WeaponsPool : MonoBehaviour
{
    public List<GameObject> weapons;
    private Random random = new ();
    public GameObject[] potions;

    public GameObject PeekRandomWeapon()
    {
        if (weapons.Count == 0)
            return potions[random.Next(2)];
        var i = random.Next(weapons.Count);
        var result = weapons[i];
        weapons.RemoveAt(i);
        return result;
    }
}
