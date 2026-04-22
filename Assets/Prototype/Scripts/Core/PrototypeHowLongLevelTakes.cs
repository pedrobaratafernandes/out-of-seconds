using UnityEngine;

public class PrototypeHowLongLevelTakes : MonoBehaviour
{
    [SerializeField] private float secondsForLevel = 30f;

    void Start()
    {
        if (PrototypeGameManager.Instance != null)
        {
            if (!PrototypeGameManager.Instance.isContinuing)
            {
                PrototypeGameManager.Instance.SetupLevel(secondsForLevel);
            }
            else
            {

                PrototypeGameManager.Instance.levelMaxTime = secondsForLevel;

                // Aplica o tempo guardado ao tempo restante real
                PrototypeGameManager.Instance.timeRemaining = PrototypeGameManager.Instance.returnTime;


                PrototypeGameManager.Instance.isContinuing = false;
                PrototypeGameManager.Instance.gameStarted = true;
            }
        }
    }
}