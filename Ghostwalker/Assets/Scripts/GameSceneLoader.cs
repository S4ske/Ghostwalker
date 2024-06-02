using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneLoader : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        SceneManager.LoadScene("Game");
    }
}
