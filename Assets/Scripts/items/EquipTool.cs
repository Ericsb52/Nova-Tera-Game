using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipTool : Equip
{

    public float attackRate;
    private bool atticking;
    public float attackDistance;

    [Header("Resource Gathering")]
    public bool doesGatherResources;

    [Header("combat")]
    public bool doesDealDamage;
    public int damage;

    [Header("components")]
    private Animator anim;
    private Camera cam;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        cam = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void attack()
    {
        base.attack();
        if (!atticking)
        {
            atticking = true;
            anim.SetTrigger("Attack");
            Invoke("OnCanAttack", attackRate);
        }
    }

    public void onCanAttack()
    {
        atticking = true;
    }

    public void onHit()
    {

    }

    public override void altAttack()
    {
        base.altAttack();
    }


}
