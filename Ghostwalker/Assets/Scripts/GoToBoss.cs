using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToBoss : MonoBehaviour
{
    public void ToBoss()
    {
        SceneManager.LoadScene("BossFight");
    }
}
