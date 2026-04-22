using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic; // para Listas

public class InventoryManager : MonoBehaviour
{

    [SerializeField] private Image keyIconUI;
    [SerializeField] private List<Image> slots;
    [SerializeField] private Sprite fullCapsuleSprite;
    [SerializeField] private Sprite emptyCapsuleSprite;
    private int currentItemIndex = 0; // indice para o proximo item a ser coletado

    private void Start()
    {
        // esconde chave ao iniciar nivel
        if (keyIconUI != null)
        {
            keyIconUI.enabled = false;
        }

        // atualiza HUB de capsulas
        UpdateCapsuleUI();
    }

    // Faz a chave aparecer
    public void ActiveKeyUI()
    {
        if (keyIconUI != null)
        {
            keyIconUI.enabled = true;

        }
    }

    
    // verificar quantas capsulas foram coletadas e atualizar a UI
    public void UpdateCapsuleUI()
    {
        int collected = GameManager.Instance.GetCapsuleCount();

        for (int i = 0; i < slots.Count; i++)
        {
            // se o indice for menor que a quantidade coletada, mostra capsula cheia, senao mostra capsula vazia
            if (i < collected)
            {
                slots[i].sprite = fullCapsuleSprite;
            }
            else if (emptyCapsuleSprite != null)
            {
                slots[i].sprite = emptyCapsuleSprite;
            }
        }

        currentItemIndex = collected;
    }
}