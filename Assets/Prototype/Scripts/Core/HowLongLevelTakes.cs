using UnityEngine;

public class HowLongLevelTakes : MonoBehaviour
{
    [SerializeField] private float secondsForLevel = 30f;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetupLevel(secondsForLevel);
        }
    }
}