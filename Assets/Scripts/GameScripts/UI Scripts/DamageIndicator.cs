using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DamageIndicator : MonoBehaviour
{
    public Image image;
    public float flashSpeed;

    private Coroutine fade_Away;

    public void flash()
    {
        if(fade_Away != null)
        {
            StopCoroutine(fade_Away);

        }
        image.enabled = true;
        image.color = Color.white;
        fade_Away = StartCoroutine(fadeAway());
    }

    IEnumerator fadeAway()
    {
        float a = 1.0f;
        while(a > 0.0f)
        {
            a -= (1.0f / flashSpeed) * Time.deltaTime;
            image.color = new Color(1.0f, 1.0f, 1.0f, a);
            yield return null;
        }
        image.enabled = false;
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
