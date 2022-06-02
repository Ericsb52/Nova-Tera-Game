using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{

    public int damage;
    public float damageRate;

    private List<IDamagable> thingsToDamage = new List<IDamagable>();


    IEnumerator dealDamage()
    {
        while (true)
        {
            foreach(IDamagable i in thingsToDamage)
            {
                i.takeDamage(damage);
            }

            yield return new WaitForSeconds(damageRate);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(dealDamage());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<IDamagable>() != null)
        {
            thingsToDamage.Add(collision.gameObject.GetComponent<IDamagable>());
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.GetComponent<IDamagable>() != null)
        {
            thingsToDamage.Remove(collision.gameObject.GetComponent<IDamagable>());
        }
    }
}
