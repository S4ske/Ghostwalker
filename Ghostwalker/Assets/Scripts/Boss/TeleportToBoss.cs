using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeleportToBoss : MonoBehaviour
{
    [SerializeField] private Text text;
    private bool opened;
    private string[] phrases = 
    {
        "Ха-ха-ха, мой наивный друг...",
        "Ты думал, кто-то станет помогать тебе просто так?!",
        "Мне лишь нужно было, чтобы ты нашёл мое тело.",
        "Здесь, рядом с ним, я наконец могу использовать свою магию...",
        "Cоединить свое тело с душой и вернуть себе настоящий облик.",
        "А покинуть это подземелье сможет только один из нас!",
        "Такова суть этой тюрьмы. И это буду я!",
        "Я забираю все твои орудия, да начнется же честная битва!"
    };
    private int i;
    private Player player;

    void Update()
    {
        if (i < phrases.Length && Input.GetKeyDown(KeyCode.Return) && opened)
        {
            if (i == phrases.Length - 1)
            {
                player.Listened = true;
                text.text = "";
                i = phrases.Length;
                SceneManager.LoadScene("BossFight");
            }
            else
            {
                i++;
                text.text = phrases[i];
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger && !opened)
            text.text = "E - open";
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger && !opened)
        {
            player = other.GetComponent<Player>();
            if (player.pressedE)
            {
                opened = true;
                player.Listened = false;
                text.text = phrases[0];
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.CompareTag("Player") && other.isTrigger && !opened)
            text.text = "";
    }
}