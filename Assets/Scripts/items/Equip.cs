using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : MonoBehaviour
{

    public virtual void attack()
    {
        Debug.Log("preform normal attack");
    }

    public virtual void altAttack()
    {
        Debug.Log("preform alt attack");
    }
}
