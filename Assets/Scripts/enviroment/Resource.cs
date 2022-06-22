using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [Header("Prefads")]
    public ItemData itemToGive;

    [Header("Rescoure")]
    public int quantityPerHit = 1;
    public int capacity;
    public GameObject hitParticle;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void gather(Vector3 hitpoint, Vector3 hitNormal)
    {
        for(int i = 0; i<quantityPerHit; i++)
        {
           
            if(capacity <= 0)
            {
                break;
            }

            capacity--;

            Inventory.instance.addItem(itemToGive);
        }

        Instantiate(hitParticle, hitpoint, Quaternion.LookRotation(hitNormal, Vector3.up));

        if (capacity <= 0)
        {
            Destroy(gameObject);
        }
    }


}
