using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    //GameObjects
    public CharacterController controller;
    public Transform playerBody; //Gets playerBody transform
    public Transform playerCamera; //Gets playerCamera transform

    //Variables
    //PlayerMovement
    private float speed = 5f; //Player speed
    private float gravity = -9.81f; //Player gravity
    private float groundDistance = 0.4f; //Radius of sphere around groundCheck
    private float jumpHeight = 1f; //Player jump height
    //MouseLook
    private float xRotation = 0f;
    private float sensitivity = 2f; //later replace with gameManager.sensitivity

    //Velocity & Checks
    Vector3 velocity; //Current player velocity
    public Transform groundCheck; //Position of groundCheck
    public LayerMask groundMask; //Layer groundMask
    bool isGrounded; //Is player currently grounded

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Lock mouse cursor to window
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
        playerJump();
        mouseLook();
    }

    public void playerMovement()
    {
        //Get Axis Inputs for movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z; //Allows analog input

        controller.Move(velocity * Time.deltaTime); //Apply gravity to player --> y = 1/2g * t^2

        if (Input.GetKey(KeyCode.LeftShift)) //Sprint if leftShift is pressed
        {
            controller.Move(move * speed * 2 * Time.deltaTime); //Double player movement speed
        }
        else //No additional movement key is pressed
        {
            controller.Move(move * speed * Time.deltaTime); //Base player movement speed
        }
    }

    public void playerJump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //Create CheckSphere around the player and check if player is currently grounded

        if (isGrounded && velocity.y < 0) //Check if player is grounded and has no velocity --> Also prevents gravity build-up
        {
            velocity.y = -2f; //Pulls player to the ground --> used to compensate for sphere distance and ensure that player does not hover over ground
        }

        if (Input.GetButtonDown("Jump") && isGrounded) //Check if player pressed jump button
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); //Add vertical velocity (Y) --> Player jumps
        }

        velocity.y += gravity * Time.deltaTime; //Multiply gravity over time

        controller.Move(velocity * Time.deltaTime); //Apply gravity to player --> y = 1/2g * t^2
    }

    public void mouseLook()
    {
        //Gets mouse axis input and multiplies by the sensitivity within GameManager.cs
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        xRotation -= mouseY; //Rotate around x-axis and flip mouse rotation input
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //Clamp player rotation

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //Apply vertical rotation to camera
        playerBody.Rotate(Vector3.up * mouseX); //Apply horizontal rotation to body
    }
}
