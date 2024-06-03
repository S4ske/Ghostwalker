using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    private string[] phrases =
    {
        "Нееееет, ах ты ж…",
        "Ну ладно, это была честная битва.",
        "Ты - маг достойный выжить.",
        "Поздравляю..."
    };
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
                text.text = "";
                i = phrases.Length;
                SceneManager.LoadScene("Menu");
            }
            else
            {
                i++;
                text.text = phrases[i];
            }
        }
    }
}
