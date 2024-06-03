using UnityEngine;
using UnityEngine.UI;

public class Mantle : MonoBehaviour
{
    private string[] phrases =
    {
        "*Вы слышите, как с вами говорит летающая мантия*\n" +
        "Enter - продолжить",
        "Опять в нашей темнице пополнение. Злобные эльфы…",
        "Здравствуй, маг, меня зовут Элдрик Стормгард.",
        "Думаю, ты видел мое имя в учебниках магии.",
        "Да, я был верховным магом, когда 100 лет назад меня, также как и тебя," +
        " поймали и бросили умирать в эту темницу.",
        "Увы, я не смог выбраться, и, чтобы избежать смерти, я использовал остатки своих сил и спрятал душу" +
        " в мое оружие - магическую мантию.",
        "Она обладает сверх способностями, поэтому надень ее. Не стесняйся.",
        "Давай вместе попробуем выбраться из этого подземелья!"
    };

    public Sprite idleWithMantle;
    [SerializeField] private Player player;
    private bool enterPressed;
    [SerializeField] private Text text;
    private int i;

    private void Start()
    {
        text.text = phrases[0];
    }

    void Update()
    {
        if (i < phrases.Length && Input.GetKeyDown(KeyCode.Return))
        {
            if (i == phrases.Length - 1)
            {
                player.Listened = true;
                text.text = "";
                i = phrases.Length;
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
        if (other.GetComponent<Player>() && other.isTrigger)
            text.text = "E - Pick Up";
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Player>() && other.isTrigger && Input.GetKey(KeyCode.E))
        {
            other.GetComponent<SpriteRenderer>().sprite = idleWithMantle;
            other.GetComponent<Player>().WithMantle = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player>() && other.isTrigger)
            text.text = string.Empty;
    }
}
