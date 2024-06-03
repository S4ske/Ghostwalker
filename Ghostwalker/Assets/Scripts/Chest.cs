using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Chest : MonoBehaviour
{
    [SerializeField] private WeaponsPool weaponsPool;
    [SerializeField] private GameObject[] potions;
    [SerializeField] private Text text;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
            text.text = "E - open chest";
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            var player = other.GetComponent<Player>();
            if (player.pressedE)
            {
                player.pressedE = false;
                player.currentECd = 0;
                Destroy(gameObject);
                var random = new Random();
                var pos = transform.position;
                Instantiate(weaponsPool.PeekRandomWeapon(), 
                    new Vector3(pos.x - 2, pos.y, 1), Quaternion.identity);
                Instantiate(potions[random.Next(potions.Length)], 
                    new Vector3(pos.x + 2, pos.y, 1), Quaternion.identity);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
            text.text = "";
    }
}
