using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonScript : MonoBehaviour {

    public GameObject GravCollider;
    public GameObject ctrlText;
    private GameObject txtref; //Used to modify the moon text
    public List<GameObject> claimedObjects;
    public Vector3 velocity;
    public Vector3 lastpos;
    public float releasetimer;

    public Sprite[] spr = new Sprite[3];

    // Use this for initialization
    void Start () {
        releasetimer = 0;
        lastpos = new Vector3(0, 0, 0);
        Instantiate(GravCollider, transform.localPosition, Quaternion.identity, this.transform);
        txtref = Instantiate(ctrlText, transform.localPosition, Quaternion.identity, this.transform);
        txtref.GetComponent<TextMesh>().text = "";
        txtref.GetComponent<TextMesh>().fontSize = 250;

        // Set random moon sprite
        int rnd = Random.Range(0, 3);
        GetComponent<SpriteRenderer>().sprite = spr[rnd];
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.tag == "Projectile")
        {
            Destroy(obj);
        }
    }
}
