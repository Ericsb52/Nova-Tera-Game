using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerNeeds : MonoBehaviour,IDamagable
{
    public Need health;
    public Need hunger;
    public Need thirst;
    public Need sleep;

    public float noHungerHealthDecay;
    public float noThirstHealthDecay;

    public UnityEvent onTakeDamage;


    // Start is called before the first frame update
    void Start()
    {
        health.curValue = health.startValue;
        hunger.curValue = hunger.startValue;
        thirst.curValue = thirst.startValue;
        sleep.curValue = sleep.startValue;
    }

    // Update is called once per frame
    void Update()
    {
        //decay needs over time

        hunger.takefrom(hunger.decayRate * Time.deltaTime);
        thirst.takefrom(thirst.decayRate * Time.deltaTime);
        sleep.addTo(sleep.regenRate * Time.deltaTime);

        //decay health if out of hunger
        if (hunger.curValue <= 0.0f)
        {
            health.takefrom(noHungerHealthDecay * Time.deltaTime);
        }

        //decay health if out of thirst
        if (thirst.curValue <= 0.0f)
        {
            health.takefrom(noThirstHealthDecay * Time.deltaTime);
        }

        // check to see if player is dead
        if(health.curValue <= 0.0f)
        {
            die();
        }

        //update UI
        health.updateUi();
        hunger.updateUi();
        sleep.updateUi();
        thirst.updateUi();
    }

    // called when player is healed
    public void heal(float amount)
    {
        health.addTo(amount);
    }

    // called when player eats
    public void eat(float amount)
    {
        hunger.addTo(amount);
    }

    // called when player drinks
    public void drink(float amount)
    {
        thirst.addTo(amount);
    }

    // called when a player rests
    public void rest(float amount)
    {
        sleep.takefrom(amount);
    }

    // called when a player takes damage
    public void takeDamage(int amount)
    {
        health.takefrom(amount);
        onTakeDamage?.Invoke();

    }

    //called if the player dies
    public void die()
    {
        Debug.Log("Player has died");
    }
}

// needs system class objects
[System.Serializable]
public class Need
{
    [HideInInspector]
    public float curValue;
    public float maxValue;
    public float startValue;
    public float regenRate;
    public float decayRate;
    public Image uiBar;


    // adds to the current value of the need
    public void addTo(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue);
    }

    // subtracks from the current value of the need
    public void takefrom(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0.0f);
    }

    // calculates the curent fill ammount
    public float getFillamount()
    {
        return curValue / maxValue;
    }
    
    // updates the fill amount bar
     public void updateUi()
    {
        uiBar.fillAmount = getFillamount();
    }
}
