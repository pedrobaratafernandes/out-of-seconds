using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{

    [SerializeField] private Image keyIconUI;
    [SerializeField] private List<Image> slots;
    private int currentItemIndex = 0;
    [SerializeField] private Sprite fullCapsuleSprite;
    [SerializeField] private Sprite emptyCapsuleSprite;
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

    // adicionar capsulas
    private void CollectCapsule()
    {
        if (currentItemIndex < slots.Count && slots[currentItemIndex] != null)
        {
            slots[currentItemIndex].sprite = fullCapsuleSprite;
            currentItemIndex++;
        }
    }

    //atualiza HUB de capsulas
    public void UpdateCapsuleUI()
    {
        int collected = PrototypeGameManager.Instance.GetCapsuleCount();

        for (int i = 0; i < slots.Count; i++)
        {
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