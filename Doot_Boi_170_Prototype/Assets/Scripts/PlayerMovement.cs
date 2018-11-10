using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject GravCollider;
    private GameObject gravref; //Used to modify the player field
    public GameObject ctrlText;
    private GameObject txtref; //Used to modify the player text

    public List<GameObject> claimedObjects;

    public int player;
    private Rigidbody2D rb; //the rigidbody of the player
    private SpriteRenderer sr; //the sprite renderer of the player
    private bool keyLeft, keyRight, keyUp, keyDown, key1, key2, key3, key4, key5, key6, keyMode;
    private float[] keytimer;
    public float movementSpeed;

    // Use this for initialization
    void Start()
    {
        claimedObjects.Add(this.gameObject);
        keytimer = new float[] { 0, 0, 0, 0, 0, 0 };
        gravref = Instantiate(GravCollider, transform.localPosition, Quaternion.identity, this.transform);
        gravref.GetComponent<SpriteRenderer>().sortingOrder = -5;
        gravref.GetComponent<SpriteRenderer>().color = new Color (0,1,1,0.3f);
        txtref = Instantiate(ctrlText, transform.localPosition, Quaternion.identity, this.transform);
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs(); //gets the player inputs
        MovePlayer();
        UseMoons();
        for(int i = 0; i < keytimer.Length; i++)
        {
            if(keytimer[i] - Time.deltaTime <= 0) { keytimer[i] = 0; }
            else { keytimer[i] -= Time.deltaTime; }
        }
    }

    //gets the inputs of the player.
    void GetInputs()
    {
        if (player == 1)
        { // if this is player 1, get player 1 controls and set text
            keyLeft = Input.GetKey(KeyCode.A);
            keyRight = Input.GetKey(KeyCode.D);
            keyUp = Input.GetKey(KeyCode.W);
            keyDown = Input.GetKey(KeyCode.S);
            key1 = Input.GetKey(KeyCode.Alpha1);
            key2 = Input.GetKey(KeyCode.Alpha2);
            key3 = Input.GetKey(KeyCode.Alpha3);
            key4 = Input.GetKey(KeyCode.Alpha4);
            key5 = Input.GetKey(KeyCode.Alpha5);
            key6 = Input.GetKey(KeyCode.Alpha6);
            keyMode = Input.GetKey(KeyCode.Space);
            txtref.GetComponent<TextMesh>().text = "1";
        }
        else if (player == 2)
        { // if this is player 2, get player 2 controls and set text
            keyLeft = Input.GetKey(KeyCode.LeftArrow);
            keyRight = Input.GetKey(KeyCode.RightArrow);
            keyUp = Input.GetKey(KeyCode.UpArrow);
            keyDown = Input.GetKey(KeyCode.DownArrow);
            key1 = Input.GetKey(KeyCode.Keypad1);
            key2 = Input.GetKey(KeyCode.Keypad2);
            key3 = Input.GetKey(KeyCode.Keypad3);
            key4 = Input.GetKey(KeyCode.Keypad4);
            key5 = Input.GetKey(KeyCode.Keypad5);
            key6 = Input.GetKey(KeyCode.Keypad6);
            keyMode = Input.GetKey(KeyCode.Keypad0);
            txtref.GetComponent<TextMesh>().text = "N1";
        }
    }

    //moves the player based on inputted keys
    void MovePlayer()
    {
        //speeds are multiplied by delta time to guarantee same movement speed regardless of fps
        if (keyLeft == true)
        {
            rb.AddForce(Vector2.left * movementSpeed * Time.deltaTime);
        }
        if (keyRight == true)
        {
            rb.AddForce(Vector2.right * movementSpeed * Time.deltaTime);
        }
        if (keyUp == true)
        {
            rb.AddForce(Vector2.up * movementSpeed * Time.deltaTime);
        }
        if (keyDown == true)
        {
            rb.AddForce(Vector2.down * movementSpeed * Time.deltaTime);
        }
    }

    //Use moons to throw projectiles or activate abilities
    void UseMoons()
    {
        if (key1 == true && keytimer[0] <= 0 && claimedObjects.Count > 1)
        {
            GameObject thrownmoon = claimedObjects[claimedObjects.Count - 1];
            thrownmoon.transform.parent = null;
            thrownmoon.GetComponent<OrbitMotion>().orbitActive = false;
            thrownmoon.transform.GetChild(1).GetComponent<TextMesh>().text = "";
            thrownmoon.GetComponent<OrbitMotion>().enabled = false;
            thrownmoon.GetComponent<MoonScript>().releasetimer = 0.5f;
            claimedObjects.RemoveAt(claimedObjects.Count - 1);
            this.transform.GetChild(0).GetComponent<GravColliderScript>().sizemod -= 1f;
            this.transform.GetChild(0).GetComponent<GravColliderScript>().updateRange();
            keytimer[0] = 1;
        }
        if (key2 == true && keytimer[1] <= 0 && claimedObjects.Count >= 2)
        {
            GameObject thismoon = claimedObjects[1];
            if (thismoon.GetComponent<MoonScript>().claimedObjects.Count > 0)
            {
                GameObject thrownprojectile = thismoon.GetComponent<MoonScript>().claimedObjects[0];
                thrownprojectile.transform.parent = null;
                thrownprojectile.GetComponent<OrbitMotion>().orbitActive = false;
                thrownprojectile.GetComponent<OrbitMotion>().enabled = false;
            }
            keytimer[1] = 1;
        }
        if (key3 == true && keytimer[2] <= 0 && claimedObjects.Count >= 3)
        {
            GameObject thismoon = claimedObjects[2];
            keytimer[2] = 1;
        }
        if (key4 == true && keytimer[3] <= 0 && claimedObjects.Count >= 4)
        {
            GameObject thismoon = claimedObjects[3];
            keytimer[3] = 1;
        }
        if (key5 == true && keytimer[4] <= 0 && claimedObjects.Count >= 5)
        {
            GameObject thismoon = claimedObjects[4];
            keytimer[4] = 1;
        }
        if (key6 == true && keytimer[5] <= 0 && claimedObjects.Count >= 6)
        {
            GameObject thismoon = claimedObjects[5];
            keytimer[5] = 1;
        }
    }
}
