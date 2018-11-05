using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public int player;
    private Rigidbody2D rb; //the rigidbody of the player
    private SpriteRenderer sr; //the sprite renderer of the player
    private bool keyLeft, keyRight, keyUp, keyDown;
    public float movementSpeed;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs(); //gets the player inputs
        MovePlayer();
    }

    //gets the inputs of the player.
    void GetInputs()
    {
        if (player == 1)
        { // if this is player 1, get player 1 controls
            keyLeft = Input.GetKey(KeyCode.A);
            keyRight = Input.GetKey(KeyCode.D);
            keyUp = Input.GetKey(KeyCode.W);
            keyDown = Input.GetKey(KeyCode.S);
        }
        else if (player == 2)
        { // if this is player 2, get player 2 controls
            keyLeft = Input.GetKey(KeyCode.LeftArrow);
            keyRight = Input.GetKey(KeyCode.RightArrow);
            keyUp = Input.GetKey(KeyCode.UpArrow);
            keyDown = Input.GetKey(KeyCode.DownArrow);
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
}
