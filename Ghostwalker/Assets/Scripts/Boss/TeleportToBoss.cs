using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeleportToBoss : MonoBehaviour
{
    [SerializeField] private Text text;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
            text.text = "E - StartBossFight";
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            var player = other.GetComponent<Player>();
            if (player.pressedE)
            {
                SceneManager.LoadScene("BossFight");
                var playerWeapons = PlayerData.Instance.weapons;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.CompareTag("Player") && other.isTrigger)
            text.text = "";
    }
}