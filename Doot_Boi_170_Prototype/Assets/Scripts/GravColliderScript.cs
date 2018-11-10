using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravColliderScript : MonoBehaviour {

    public string type;
    private GameObject parent;
    private float sizemod;

	// Use this for initialization
	void Start () {
        // Get parent object and check the tag to see what this gravity field affects.
        parent = this.transform.parent.gameObject;
        if (parent.tag == "Player")
        {
            type = "PlayerField";
            this.GetComponent<CircleCollider2D>().radius = 0.5f * (((2f + sizemod - .3f) / 2) / ((2f + sizemod) / 2));
        }
        else
        {
            type = "MoonField";
            this.GetComponent<CircleCollider2D>().radius = 0.5f * (((2f + sizemod - .1f) / 2) / ((2f + sizemod) / 2));
        }
        sizemod = 0f;
        transform.localScale = new Vector3(2f, 2f, 1);
        transform.localPosition = new Vector3(0, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (type == "PlayerField" && obj.tag == "Collectable")
        {
            obj.transform.parent = parent.transform;
            OrbitMotion newcircle = obj.GetComponent<OrbitMotion>();
            newcircle.orbitPath = new Ellipse((2f+sizemod)/2, (2f+sizemod)/2);
            newcircle.orbitProgress = FindDegree(obj.transform.localPosition.x, obj.transform.localPosition.y)/360;
            newcircle.enabled = true;
            sizemod += 1f;
            newcircle.orbitPeriod = sizemod + 3f;
            this.transform.localScale = new Vector3(2f + sizemod, 2f + sizemod, 1);
            this.GetComponent<CircleCollider2D>().radius = 0.5f*(((2f+sizemod-.3f)/2)/((2f+sizemod)/2));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (type == "MoonField" && obj.tag == "Projectile")
        { 
            obj.transform.parent = parent.transform;
            OrbitMotion newcircle = obj.GetComponent<OrbitMotion>();
            newcircle.orbitPath = new Ellipse(1f, 1f);
            newcircle.orbitProgress = FindDegree(obj.transform.localPosition.x, obj.transform.localPosition.y) / 360;
            newcircle.orbitPeriod = 2f;
            newcircle.enabled = true;
        }
    }

    private float FindDegree(float x, float y)
    {
        float value = (float)((System.Math.Atan2(x, y) / System.Math.PI) * 180f);
        if (value < 0) value += 360f;
        return value;
    }
}
