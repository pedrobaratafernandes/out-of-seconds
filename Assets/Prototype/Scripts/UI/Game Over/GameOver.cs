using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private bool isExiting = false;
    public void GoToMainMenu()
    {
        if (isExiting)
        {
            return;
        }
        isExiting = true;
        SceneManager.LoadScene("Main Menu");
    }
}
