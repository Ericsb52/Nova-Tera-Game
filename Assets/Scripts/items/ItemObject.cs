using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour,IInteractable
{
    public ItemData data;

    public string getInteractPrompt()
    {
        return string.Format("Pickup {0}", data.displayname);
    }

    public void onInteract()
    {
        Inventory.instance.addItem(data);
        Destroy(gameObject);
    }
}
