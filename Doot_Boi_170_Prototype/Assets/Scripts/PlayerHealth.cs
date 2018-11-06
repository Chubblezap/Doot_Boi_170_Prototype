// Works Cited
// https://www.youtube.com/watch?v=GfuxWs6UAJQ

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }

    public Slider healthBar;
	
	void Start () {
        MaxHealth = 100f;
        CurrentHealth = MaxHealth;

        healthBar.value = CalculateHealth();
	}
    
	void Update () {
        if (Input.GetKeyDown(KeyCode.X)) DealDamage(6);
	}

    void DealDamage(float damageValue)
    {
        CurrentHealth -= damageValue;
        healthBar.value = CalculateHealth();
        if (CurrentHealth <= 0) Die();
    }

    float CalculateHealth()
    {
        return CurrentHealth / MaxHealth;
    }

    void Die()
    {
        CurrentHealth = 0;
        Debug.Log("You are dead.");
    }
}
