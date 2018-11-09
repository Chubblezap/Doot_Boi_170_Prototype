//Code base provided via Board to Bits https://www.youtube.com/watch?v=lKfqi52PqHk&t=949s&frags=pl%2Cwn
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMotion : MonoBehaviour {

    public Transform orbitingObject;
    public Ellipse orbitPath;
    public float test;

    [Range(0f, 1f)]
    public float orbitProgress;
    public float orbitPeriod = 5f;
    public bool orbitActive = true;

	// Use this for initialization
	void Start () {
        SetOrbitingObjectPosition();
        StartCoroutine(AnimateOrbit());
	}

    void SetOrbitingObjectPosition() {
        Vector2 orbitPos = orbitPath.Evaluate(orbitProgress);
        transform.localPosition = new Vector3(orbitPos.x, orbitPos.y, 0f);
    }
	
    IEnumerator AnimateOrbit() {
        //Protect against division by 0
        if(orbitPeriod < 0.1f) {
            orbitPeriod = 0.1f;
        }
        float orbitSpeed = 1f / orbitPeriod;
        while (orbitActive) {
            orbitProgress += Time.deltaTime * orbitSpeed;
            orbitProgress %= 1f;
            SetOrbitingObjectPosition();
            yield return null; 
        }
    }
}
