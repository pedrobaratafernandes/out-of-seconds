using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{

    public Image keyIconUI;
    public List<Image> slots;
    private int currentItemIndex = 0;
    public Sprite fullCapsuleSprite;
    public Sprite emptyCapsuleSprite;
    void Start()
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
    public void CollectCapsule()
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
        int collected = GameManager.Instance.GetCapsuleCount();

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