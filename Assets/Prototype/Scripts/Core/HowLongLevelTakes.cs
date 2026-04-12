using UnityEngine;

public class HowLongLevelTakes : MonoBehaviour
{
    [SerializeField] private float secondsForLevel = 30f;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            if (!GameManager.Instance.isContinuing)
            {
                GameManager.Instance.SetupLevel(secondsForLevel);
            }
            else
            {
                
                GameManager.Instance.levelMaxTime = secondsForLevel;

                // Aplica o tempo guardado ao tempo restante real
                GameManager.Instance.timeRemaining = GameManager.Instance.returnTime;

                
                GameManager.Instance.isContinuing = false;
                GameManager.Instance.gameStarted = true;
            }
        }
    }
}