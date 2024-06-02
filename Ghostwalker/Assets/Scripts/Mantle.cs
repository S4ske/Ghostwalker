using System.Collections;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Mantle : MonoBehaviour
{
    private string[] phrases =
    {
        "*Вы слышите, как с вами говорит летающая мантия*",
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
    
    public bool isSpeaking;
    [SerializeField] private Player player;
    private bool enterPressed;
    private StringBuilder phrase = new ();
    [SerializeField] private Text text;
    private IEnumerator enumerator;
    private int i;

    private void Start()
    {
        enumerator = TextCoroutine(phrases[0]);
    }
    
    void Update()
    {
        if (isSpeaking)
        {
            if (enumerator.MoveNext())
            {
                if (Input.GetKeyDown(KeyCode.Return) || enterPressed)
                {
                    phrase.Append(enumerator.Current);
                    while (enumerator.MoveNext())
                    {
                        phrase.Append(enumerator.Current);
                    }

                    text.text = phrase.ToString();
                }
                else
                {
                    phrase.Append(enumerator.Current);
                    text.text = phrase.ToString();
                    Thread.Sleep(100);
                }
            }
            else
            {
                isSpeaking = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return) || enterPressed)
        {
            if (i + 1 == phrases.Length)
            {
                player.Listened = true;
                text.text = "";
            }
            else
            {
                i++;
                isSpeaking = true;
                enumerator = TextCoroutine(phrases[i]);
                phrase = new StringBuilder();
            }
        }

        enterPressed = false;
    }
    
    private IEnumerator TextCoroutine(string text)
    {
        foreach(var c in text)
        {
            yield return c;
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
