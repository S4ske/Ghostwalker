using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicSkip : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene("Game");
    }
}
