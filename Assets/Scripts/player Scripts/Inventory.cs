using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using TMPro;

public class Inventory : MonoBehaviour
{

    public ItemSlotUI[] uiSlots;
    public ItemSlot[] slots;

    public GameObject inventoryWindow;
    public Transform dropPosition;

    [Header("Selected Item")]
    private ItemSlot selectedItem;
    private int selectedItemIndex;

    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedItemStatNames;
    public TextMeshProUGUI selectedItemStatValues;

    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unequipButton;
    public GameObject dropButton;

    private int curEquipIndex;

    private PlayerControler controller;
    private PlayerNeeds needs;

    [Header("Events")]
    public UnityEvent onOpenInventory;
    public UnityEvent onCloseInventory;

    public static Inventory instance;

    private void Awake()
    {
        instance = this;
        controller = GetComponent<PlayerControler>();
        needs = GetComponent<PlayerNeeds>();
    }


    // Start is called before the first frame update
    void Start()
    {
        inventoryWindow.SetActive(false);
        slots = new ItemSlot[uiSlots.Length];
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i] = new ItemSlot();
            uiSlots[i].index = i;
            uiSlots[i].clear();

        }
        clearSelectedItemWindow();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onInventoryButton(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            toggle();
        }
    }

    public void toggle()
    {
        if (inventoryWindow.activeInHierarchy)
        {
            inventoryWindow.SetActive(false);
            onCloseInventory.Invoke();
            controller.toggleCursor(false);
        }
        else
        {
            inventoryWindow.SetActive(true);
            onOpenInventory.Invoke();
            clearSelectedItemWindow();
            controller.toggleCursor(true);
        }
    }

    public bool isOpen()
    {
        return inventoryWindow.activeInHierarchy;  
    }

    public void addItem(ItemData item)
    {
        if (item.canStack)
        {
            ItemSlot slotToStack = GetItemStack(item);

            if (slotToStack != null)
            {
                slotToStack.quantity++;
                updateUi();
                return;
            }
        }
        ItemSlot emptySlot = getEmptySlot();
        if (emptySlot != null)
        {
            emptySlot.item = item;
            emptySlot.quantity = 1;
            updateUi();
            return ;
        }
        throwItem(item);
    }
    public void throwItem(ItemData item)
    {
        Instantiate(item.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360.0f));
    }

    public void updateUi()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                uiSlots[i].set(slots[i]);
            }
            else
            {
                uiSlots[i].clear();
            }
        }
    }

    ItemSlot GetItemStack(ItemData item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == item && slots[i].quantity < item.maxStackAmount)
            {
                return slots[i];
            }
        }
            return null;
    }

    ItemSlot getEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].item == null)
            {
                return slots[i];
            }
        }
        return null;
    }

    public void selectItem(int index)
    {
        if (slots[index].item == null)
        {
            return;
        }
        selectedItem = slots[index];
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.item.displayname;
        selectedItemDescription.text = selectedItem.item.description;

        //set stat names and values
        selectedItemStatNames.text = string.Empty;
        selectedItemStatValues.text = string.Empty;

        for(int i = 0; i < selectedItem.item.consumables.Length; i++)
        {
            selectedItemStatNames.text += selectedItem.item.consumables[i].type.ToString()+"\n";
            selectedItemStatValues.text += selectedItem.item.consumables[i].value.ToString() + "\n";

        }

        useButton.SetActive(selectedItem.item.type == ItemType.Consumable);
        equipButton.SetActive(selectedItem.item.type == ItemType.Equipable && !uiSlots[index].equipred);
        unequipButton.SetActive(selectedItem.item.type == ItemType.Equipable && uiSlots[index].equipred);

        dropButton.SetActive(true);

    }

    public void clearSelectedItemWindow()
    {
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        selectedItemStatNames.text = string.Empty;
        selectedItemStatValues.text = string.Empty;

        useButton.SetActive(false);
        equipButton.SetActive(false);
        unequipButton.SetActive(false);
        dropButton.SetActive(false);

    }

    public void onUseButton()
    {
        if(selectedItem.item.type == ItemType.Consumable)
        {
            for(int i = 0; i < selectedItem.item.consumables.Length; i++)
            {
                switch (selectedItem.item.consumables[i].type)
                {
                    case ConsumableType.Health:
                        needs.heal(selectedItem.item.consumables[i].value);
                        break;
                    case ConsumableType.Hunger:
                        needs.eat(selectedItem.item.consumables[i].value);
                        break;
                    case ConsumableType.Thirst:
                        needs.drink(selectedItem.item.consumables[i].value);
                        break;
                    case ConsumableType.Sleep:
                        needs.rest(selectedItem.item.consumables[i].value);
                        break;
                }
            }
        }
        removeSelectedItem();
    }

    public void onUnEquipButton()
    {

    }
    void unEquip(int index)
    {

    }

    public void onEquipButton()
    {

    }

    public void onDropButton()
    {
        throwItem(selectedItem.item);
        removeSelectedItem();
    }

    void removeSelectedItem()
    {
        selectedItem.quantity--;
        if (selectedItem.quantity == 0)
        {
            if (uiSlots[selectedItemIndex].equipred == true)
            {
                unEquip(selectedItemIndex);
            }
            selectedItem.item = null;   
            clearSelectedItemWindow();
            
        }
        updateUi();
    }

    void removeItem(ItemData item)
    {

    }

    public bool hasItem(ItemData item, int quantity)
    {
        return false;
    }
}

public class ItemSlot
{

    public ItemData item;
    public int quantity;
}
