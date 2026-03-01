using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System;

public class MainMenu : MonoBehaviour
{
    
    public TextMeshProUGUI timeText;

    
    public TextMeshProUGUI startText;
    public TextMeshProUGUI quitText;

    
    public Color unselectedColor = Color.white;
    public Color selectedColor = Color.green;

    private int selectedIndex = 0; // 0 = Start, 1 = Quit
    private string startOriginal;
    private string quitOriginal;

    [Header("New Input System")]
    public InputAction navigateAction;
    public InputAction submitAction;

    private bool navigated = false;

    void OnEnable()
    {
        navigateAction.Enable();
        submitAction.Enable();
    }

    void OnDisable()
    {
        navigateAction.Disable();
        submitAction.Disable();
    }

    void Start()
    {
        startOriginal = startText.text;
        quitOriginal = quitText.text;
        UpdateSelection();
    }

    void Update()
    {
        
        
            timeText.text = DateTime.Now.ToString("HH:mm:ss");

        
        Vector2 nav = navigateAction.ReadValue<Vector2>();
        if (!navigated)
        {
            if (nav.y > 0.5f) // up
            {
                selectedIndex = 0;
                UpdateSelection();
                navigated = true;
            }
            else if (nav.y < -0.5f) // down
            {
                selectedIndex = 1;
                UpdateSelection();
                navigated = true;
            }
        }
        if (Mathf.Abs(nav.y) < 0.1f)
            navigated = false;

        // Seleção
        if (submitAction.triggered)
        {
            if (selectedIndex == 0)
                OnStartClicked();
            else
                OnQuitClicked();
        }

        // Mouse hover
        CheckMouseHover(startText, 0);
        CheckMouseHover(quitText, 1);
    }

    void UpdateSelection()
    {
        
        startText.text = startOriginal;
        startText.color = unselectedColor;

        quitText.text = quitOriginal;
        quitText.color = unselectedColor;

        
        if (selectedIndex == 0)
        {
            startText.text = $"<u>{startOriginal}</u>";
            startText.color = selectedColor;
        }
        else if (selectedIndex == 1)
        {
            quitText.text = $"<u>{quitOriginal}</u>";
            quitText.color = selectedColor;
        }
    }

    void CheckMouseHover(TextMeshProUGUI text, int index)
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();

 
        if (RectTransformUtility.RectangleContainsScreenPoint(text.rectTransform, mousePos))
        {
            if (selectedIndex != index)
            {
                selectedIndex = index;
                UpdateSelection();
            }
        }
    }

    void OnStartClicked()
    {
 
    }

    void OnQuitClicked()
    {
        Application.Quit();
    }
}