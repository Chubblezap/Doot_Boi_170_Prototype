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
        MaxHealth = 20f;
        CurrentHealth = MaxHealth;

        healthBar.value = CalculateHealth();
	}
    
	void Update () {
        if (Input.GetKeyDown(KeyCode.X)) DealDamage(6);
	}

    // If you uncomment this code then CollisionTest works
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
        //GameObject obj = collision.gameObject;
        //if (obj.tag == "Projectile")
        //{
        //    DealDamage(6);
        //    Destroy(obj);
        //}
    //}

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
