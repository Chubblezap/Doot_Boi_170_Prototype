using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonScript : MonoBehaviour {

    public GameObject GravCollider;

	// Use this for initialization
	void Start () {
        Instantiate(GravCollider, new Vector3(0, 0, 0), Quaternion.identity, this.transform);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.tag == "Projectile")
        {
        //    Destroy(obj);
        }
    }
}
