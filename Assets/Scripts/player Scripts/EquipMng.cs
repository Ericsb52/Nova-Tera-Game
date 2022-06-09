using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EquipMng : MonoBehaviour
{

    public Equip curEquip;
    public Transform equipParent;

    //for testing
    //public ItemData testItem;

    private PlayerControler controler;


    public static EquipMng instance;


    private void Awake()
    {
        instance = this;
        controler = GetComponent<PlayerControler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //for testing 
        //equipNew(testItem);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onAttackInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed && curEquip != null && controler.canLook == true)
        {
           curEquip.attack();
        }
    }

    public void onAltAttackInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && curEquip != null && controler.canLook == true)
        {
            curEquip.altAttack();
        }
    }

    public void equipNew(ItemData item)
    {
        unEquip();
        curEquip = Instantiate(item.equipPrefab, equipParent).GetComponent<Equip>();
    }

    public void unEquip()
    {
        if(curEquip != null)
        {
            Destroy(curEquip.gameObject);
            curEquip = null;
        }
    }
}
