using System;
using UnityEngine;

public class HowLongLevelTakes : MonoBehaviour
{
    [SerializeField] private float secondsForLevel = 30f;
    [SerializeField] private int level; // nome do nivel para mostrar no console
    [SerializeField] private string sceneName;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            if (!GameManager.Instance.isContinuing)
            {
                GameManager.Instance.SetupLevel(level, sceneName, secondsForLevel);
            }
            else
            {
                // Aplica o tempo guardado ao tempo restante real
                GameManager.Instance.timeRemaining = GameManager.Instance.returnTime;
                GameManager.Instance.isContinuing = false;
                GameManager.Instance.gameStarted = true;
            }
        }
    }
}