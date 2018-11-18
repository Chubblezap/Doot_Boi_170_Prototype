using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private bool[] keyWasPressed;
    public float movementSpeed;

    // Use this for initialization
    void Start()
    {
        claimedObjects.Add(this.gameObject);
        keytimer = new float[] { 0, 0, 0, 0, 0, 0 };
        keyWasPressed = new bool[] { false, false, false, false, false, false };
        gravref = Instantiate(GravCollider, transform.localPosition, Quaternion.identity, this.transform);
        gravref.GetComponent<SpriteRenderer>().sortingOrder = -5;
        gravref.GetComponent<SpriteRenderer>().color = new Color (0,1,1,0.7f);
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
        if (key1 == true)
        {
            keyWasPressed[0] = true;
        }
        else if (keyWasPressed[0] == true && key1 == false && keytimer[0] <= 0 && claimedObjects.Count > 1)
        {
            keyWasPressed[0] = false;
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
        if (key2 == true)
        {
            keyWasPressed[1] = true;
            GameObject thismoon = claimedObjects[1];
            while (thismoon.GetComponent<MoonScript>().claimedObjects[thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1] == null)
            {
                thismoon.GetComponent<MoonScript>().claimedObjects.RemoveAt(thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1);
            }
            if (thismoon.GetComponent<MoonScript>().claimedObjects.Count > 0)
            {
            //    thismoon.GetComponent<MoonScript>().claimedObjects[thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1].GetComponent<ProjScript>().arrowref.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else if (keyWasPressed[1] == true && key2 == false && keytimer[1] <= 0 && claimedObjects.Count >= 2)
        {
            keyWasPressed[1] = false;
            GameObject thismoon = claimedObjects[1];
            while (thismoon.GetComponent<MoonScript>().claimedObjects[thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1] == null)
            {
                thismoon.GetComponent<MoonScript>().claimedObjects.RemoveAt(thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1);
            }
            if (thismoon.GetComponent<MoonScript>().claimedObjects.Count > 0)
            {
                GameObject thrownprojectile = thismoon.GetComponent<MoonScript>().claimedObjects[thismoon.GetComponent<MoonScript>().claimedObjects.Count-1];
                thrownprojectile.transform.parent = null;
                thrownprojectile.GetComponent<OrbitMotion>().orbitActive = false;
                thrownprojectile.GetComponent<OrbitMotion>().enabled = false;
                thrownprojectile.GetComponent<ProjScript>().releasetimer = 0.5f;
                thismoon.GetComponent<MoonScript>().claimedObjects.RemoveAt(thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1);
            }
            keytimer[1] = 1;
        }
        if (key3 == true)
        {
            keyWasPressed[2] = true;
            GameObject thismoon = claimedObjects[2];
            while (thismoon.GetComponent<MoonScript>().claimedObjects[thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1] == null)
            {
                thismoon.GetComponent<MoonScript>().claimedObjects.RemoveAt(thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1);
            }
            if (thismoon.GetComponent<MoonScript>().claimedObjects.Count > 0)
            {
            //    thismoon.GetComponent<MoonScript>().claimedObjects[thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1].GetComponent<ProjScript>().arrowref.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else if (keyWasPressed[2] == true && key3 == false && keytimer[2] <= 0 && claimedObjects.Count >= 3)
        {
            keyWasPressed[2] = false;
            GameObject thismoon = claimedObjects[2];
            while (thismoon.GetComponent<MoonScript>().claimedObjects[thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1] == null)
            {
                thismoon.GetComponent<MoonScript>().claimedObjects.RemoveAt(thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1);
            }
            if (thismoon.GetComponent<MoonScript>().claimedObjects.Count > 0)
            {
                GameObject thrownprojectile = thismoon.GetComponent<MoonScript>().claimedObjects[thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1];
                thrownprojectile.transform.parent = null;
                thrownprojectile.GetComponent<OrbitMotion>().orbitActive = false;
                thrownprojectile.GetComponent<OrbitMotion>().enabled = false;
                thrownprojectile.GetComponent<ProjScript>().releasetimer = 0.5f;
                thismoon.GetComponent<MoonScript>().claimedObjects.RemoveAt(thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1);
            }
            keytimer[2] = 1;
        }
        if (key4 == true)
        {
            keyWasPressed[3] = true;
            GameObject thismoon = claimedObjects[3];
            while (thismoon.GetComponent<MoonScript>().claimedObjects[thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1] == null)
            {
                thismoon.GetComponent<MoonScript>().claimedObjects.RemoveAt(thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1);
            }
            if (thismoon.GetComponent<MoonScript>().claimedObjects.Count > 0)
            {
            //    thismoon.GetComponent<MoonScript>().claimedObjects[thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1].GetComponent<ProjScript>().arrowref.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else if (keyWasPressed[3] == true && key4 == false && keytimer[3] <= 0 && claimedObjects.Count >= 4)
        {
            keyWasPressed[3] = false;
            GameObject thismoon = claimedObjects[3];
            while (thismoon.GetComponent<MoonScript>().claimedObjects[thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1] == null)
            {
                thismoon.GetComponent<MoonScript>().claimedObjects.RemoveAt(thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1);
            }
            if (thismoon.GetComponent<MoonScript>().claimedObjects.Count > 0)
            {
                GameObject thrownprojectile = thismoon.GetComponent<MoonScript>().claimedObjects[thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1];
                thrownprojectile.transform.parent = null;
                thrownprojectile.GetComponent<OrbitMotion>().orbitActive = false;
                thrownprojectile.GetComponent<OrbitMotion>().enabled = false;
                thrownprojectile.GetComponent<ProjScript>().releasetimer = 0.5f;
                thismoon.GetComponent<MoonScript>().claimedObjects.RemoveAt(thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1);
            }
            keytimer[3] = 1;
        }
        if (key5 == true)
        {
            keyWasPressed[4] = true;
            GameObject thismoon = claimedObjects[4];
            while (thismoon.GetComponent<MoonScript>().claimedObjects[thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1] == null)
            {
                thismoon.GetComponent<MoonScript>().claimedObjects.RemoveAt(thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1);
            }
            if (thismoon.GetComponent<MoonScript>().claimedObjects.Count > 0)
            {
            //    thismoon.GetComponent<MoonScript>().claimedObjects[thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1].GetComponent<ProjScript>().arrowref.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else if (keyWasPressed[4] == true && key5 == false && keytimer[4] <= 0 && claimedObjects.Count >= 5)
        {
            keyWasPressed[4] = false;
            GameObject thismoon = claimedObjects[4];
            while (thismoon.GetComponent<MoonScript>().claimedObjects[thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1] == null)
            {
                thismoon.GetComponent<MoonScript>().claimedObjects.RemoveAt(thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1);
            }
            if (thismoon.GetComponent<MoonScript>().claimedObjects.Count > 0)
            {
                GameObject thrownprojectile = thismoon.GetComponent<MoonScript>().claimedObjects[thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1];
                thrownprojectile.transform.parent = null;
                thrownprojectile.GetComponent<OrbitMotion>().orbitActive = false;
                thrownprojectile.GetComponent<OrbitMotion>().enabled = false;
                thrownprojectile.GetComponent<ProjScript>().releasetimer = 0.5f;
                thismoon.GetComponent<MoonScript>().claimedObjects.RemoveAt(thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1);
            }
            keytimer[4] = 1;
        }
        if (key6 == true)
        {
            keyWasPressed[5] = true;
            GameObject thismoon = claimedObjects[5];
            while (thismoon.GetComponent<MoonScript>().claimedObjects[thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1] == null)
            {
                thismoon.GetComponent<MoonScript>().claimedObjects.RemoveAt(thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1);
            }
            if (thismoon.GetComponent<MoonScript>().claimedObjects.Count > 0)
            {
            //    thismoon.GetComponent<MoonScript>().claimedObjects[thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1].GetComponent<ProjScript>().arrowref.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else if (keyWasPressed[5] == true && key6 == false && keytimer[5] <= 0 && claimedObjects.Count >= 6)
        {
            keyWasPressed[5] = false;
            GameObject thismoon = claimedObjects[5];
            while (thismoon.GetComponent<MoonScript>().claimedObjects[thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1] == null)
            {
                thismoon.GetComponent<MoonScript>().claimedObjects.RemoveAt(thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1);
            }
            if (thismoon.GetComponent<MoonScript>().claimedObjects.Count > 0)
            {
                GameObject thrownprojectile = thismoon.GetComponent<MoonScript>().claimedObjects[thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1];
                thrownprojectile.transform.parent = null;
                thrownprojectile.GetComponent<OrbitMotion>().orbitActive = false;
                thrownprojectile.GetComponent<OrbitMotion>().enabled = false;
                thrownprojectile.GetComponent<ProjScript>().releasetimer = 0.5f;
                thismoon.GetComponent<MoonScript>().claimedObjects.RemoveAt(thismoon.GetComponent<MoonScript>().claimedObjects.Count - 1);
            }
            keytimer[5] = 1;
        }
    }
}
