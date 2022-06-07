using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotUI : MonoBehaviour
{
    public Button button;
    public Image icon;
    public TextMeshProUGUI QTtext;
    private ItemSlot curSlot;
    private Outline outline;

    public int index;
    public bool equipred;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void OnEnable()
    {
        outline.enabled = equipred;
    }

    public void set(ItemSlot slot)
    {
        curSlot = slot;
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.icon;
        QTtext.text = slot.quantity > 1 ? slot.quantity.ToString() : string.Empty;

        if(outline != null)
        {
            outline.enabled = equipred;
        }


    }

    public void clear()
    {
        curSlot = null;
        icon.gameObject.SetActive(false);   
        QTtext.text = string.Empty;
    }

    public void onButtonClick()
    {
        Inventory.instance.selectItem(index);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
