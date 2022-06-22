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
            Invoke("onCanAttack", attackRate);
        }
    }

    public override void altAttack()
    {
        base.altAttack();
    }

    public void onCanAttack()
    {
        atticking = false;
    }

    public void onHit()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit, attackDistance))
        {
            //hit Resorce
            if(doesGatherResources && hit.collider.GetComponent<Resource>())
            {
                hit.collider.GetComponent<Resource>().gather(hit.point, hit.normal);
            }
            //hit Enemy
            if(doesDealDamage && hit.collider.GetComponent<IDamagable>() != null)
            {
                hit.collider.GetComponent<IDamagable>().takeDamage(damage);
            }
        }
        Debug.Log("hit detected");
    }

   


}
