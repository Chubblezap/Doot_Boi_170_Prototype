using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjScript : MonoBehaviour {

    public Vector3 velocity;
    public Vector3 lastpos;
    public float releasetimer;
    //public GameObject arrow;
    //public GameObject arrowref;


    // Use this for initialization
    void Start () {
        releasetimer = 0;
        lastpos = new Vector3(0, 0, 0);
        //arrowref = Instantiate(arrow, transform.localPosition, Quaternion.identity, this.transform);
        //arrowref.GetComponent<SpriteRenderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (releasetimer - Time.deltaTime >= 0) { releasetimer -= Time.deltaTime; }
        else { releasetimer = 0; }
        if (GetComponent<OrbitMotion>().orbitActive == true)
        {
            velocity.x = ((transform.position.x) - lastpos.x) / Time.deltaTime;
            velocity.y = -1 * ((transform.position.y) - lastpos.y) / Time.deltaTime;
            velocity.z = ((transform.position.z) - lastpos.z) / Time.deltaTime;
            lastpos.x = transform.position.x;
            lastpos.y = transform.position.y;
            lastpos.z = transform.position.z;
            //arrowref.transform.eulerAngles = new Vector3(0, 0, Vector3.Angle(this.transform.forward, velocity));
        }
        else
        {
            transform.Translate(Time.deltaTime * velocity.x, Time.deltaTime * velocity.y, 0);
            //if (arrowref.GetComponent<SpriteRenderer>().enabled == true)
            //{
            //    arrowref.GetComponent<SpriteRenderer>().enabled = false;
            //}
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.tag == "Projectile")
        {
            Destroy(obj);
            Destroy(this.gameObject);
        }
    }

}
