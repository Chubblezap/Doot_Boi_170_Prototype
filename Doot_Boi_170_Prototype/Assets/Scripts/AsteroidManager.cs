﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour {

    public GameObject Asteroid;
    public float spawnTime;
    public Transform[] spawnLocation;
    public Aster[] asteroids;
    private int rndInt;


    // Use this for initialization
    void Start() {
        //Repeat the spawn every 3 sec
        InvokeRepeating("Spawn", spawnTime, spawnTime);
	}

    // Update is called once per frame
    void Update()
    {
        asteroids = FindObjectsOfType<Aster>();


        foreach (Aster rock in asteroids)
        {
            float rndF = Random.Range(0.1f, 1.0f);
            rock.transform.Translate(Time.deltaTime * rndF, Time.deltaTime * rndF, 0);
        }
    }

    // Function to spawn a new asteroid
    void Spawn() {
        rndInt = Random.Range(0, spawnLocation.Length);
        GameObject newMoon = Instantiate(Asteroid, spawnLocation[rndInt].position, spawnLocation[rndInt].rotation);
        newMoon.GetComponent<OrbitMotion>().enabled = false;
	}
}
