using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void GoToMainMenu()
    {
        GameManager.Instance.currentCapsulesCollected = 0;
        SceneManager.LoadScene("Main Menu");

    }
}
