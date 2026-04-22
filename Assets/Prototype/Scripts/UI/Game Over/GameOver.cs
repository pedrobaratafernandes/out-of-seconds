using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void GoToMainMenu()
    {
        PrototypeGameManager.Instance.currentCapsulesCollected = 0;
        SceneManager.LoadScene("Main Menu");

    }
}
