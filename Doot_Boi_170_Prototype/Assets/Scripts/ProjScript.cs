using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjScript : MonoBehaviour {

    public Vector3 velocity;
    public Vector3 lastpos;
    public float releasetimer;

    // Use this for initialization
    void Start () {
        releasetimer = 0;
        lastpos = new Vector3(0, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        if (releasetimer - Time.deltaTime >= 0) { releasetimer -= Time.deltaTime; }
        else { releasetimer = 0; }
        if (GetComponent<OrbitMotion>().orbitActive == true)
        {
            velocity = (transform.TransformPoint(Vector3.zero) - lastpos) / Time.deltaTime;
            lastpos = transform.TransformPoint(Vector3.zero);
        }
        else
        {
            transform.Translate(Time.deltaTime * velocity.x, Time.deltaTime * velocity.y, 0);
        }
    }
}
