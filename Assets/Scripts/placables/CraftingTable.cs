using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : MonoBehaviour, IInteractable
{
    public Crafting_Window craftingWindow;
    private PlayerControler player;

    

    public string getInteractPrompt()
    {
        return "Craft";
    }

    public void onInteract()
    {
        craftingWindow.gameObject.SetActive(true);
        player.toggleCursor(true);
    }

 
    void Start()
    {
        craftingWindow = FindObjectOfType<Crafting_Window>(true);
        player = FindObjectOfType<PlayerControler>();
    }

    
}
