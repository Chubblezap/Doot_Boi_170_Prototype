using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravColliderScript : MonoBehaviour {

    public string type;
    private GameObject parent;
    public float sizemod;

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
        if (type == "PlayerField" && obj.tag == "Collectable" && obj.GetComponent<OrbitMotion>().orbitActive == false && obj.GetComponent<MoonScript>().releasetimer == 0)
        {
            //Add the moon to the orbit
            obj.transform.parent = parent.transform;
            OrbitMotion newcircle = obj.GetComponent<OrbitMotion>();
            newcircle.orbitPath = new Ellipse((2f+sizemod)/2, (2f+sizemod)/2);
            newcircle.orbitProgress = FindDegree(obj.transform.localPosition.x, obj.transform.localPosition.y)/360;
            newcircle.orbitActive = true;
            sizemod += 1f;
            newcircle.orbitPeriod = sizemod + 3f;
            newcircle.enabled = true;
            //Add the moon to the planet controls
            this.parent.GetComponent<PlayerMovement>().claimedObjects.Add(obj);
            if (this.parent.GetComponent<PlayerMovement>().player == 1)
            {
                obj.transform.GetChild(1).GetComponent<TextMesh>().text = this.parent.GetComponent<PlayerMovement>().claimedObjects.Count.ToString();
            }
            else if (this.parent.GetComponent<PlayerMovement>().player == 2)
            {
                obj.transform.GetChild(1).GetComponent<TextMesh>().text = "N" + this.parent.GetComponent<PlayerMovement>().claimedObjects.Count.ToString();
            }
            updateRange();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
<<<<<<< HEAD
        if (type == "MoonField" && obj.tag == "Projectile" && obj.GetComponent<OrbitMotion>().orbitActive == false && obj.GetComponent<ProjScript>().releasetimer == 0)
        {
            //Add the projectile to the orbit
=======
        if (type == "MoonField" && obj.tag == "Projectile")
        { 
>>>>>>> c4ff064df462cb459e652effab600f19396eb2b3
            obj.transform.parent = parent.transform;
            OrbitMotion newcircle = obj.GetComponent<OrbitMotion>();
            newcircle.orbitPath = new Ellipse(1f, 1f);
            newcircle.orbitProgress = FindDegree(obj.transform.localPosition.x, obj.transform.localPosition.y) / 360;
            newcircle.orbitPeriod = 2f;
            newcircle.orbitActive = true;
            newcircle.enabled = true;
            //Add the projectile to the moon list
            this.parent.GetComponent<MoonScript>().claimedObjects.Add(obj);
        }
    }

    private float FindDegree(float x, float y)
    {
        float value = (float)((System.Math.Atan2(x, y) / System.Math.PI) * 180f);
        if (value < 0) value += 360f;
        return value;
    }

    public void updateRange()
    {
        this.transform.localScale = new Vector3(2f + sizemod, 2f + sizemod, 1);
        this.GetComponent<CircleCollider2D>().radius = 0.5f * (((2f + sizemod - .3f) / 2) / ((2f + sizemod) / 2));
    }
}
