using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonPlayerController : MonoBehaviour
{
    //camera/character rotation
    private float xRotation = 0f;
    public float mouseSensitivity = 100f;
    public Transform head;

    //character movement
    public float movingSpeed = 12f;
    public CharacterController controller;
    public float jumpHeight = 0.1f;

    //gravity
    Vector3 velocity;
    bool isGrounded;
    public float gravity = 9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        float playerX = Input.GetAxis("Horizontal");
        float playerZ = Input.GetAxis("Vertical");

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //character movement
        Vector3 playerMove = transform.right * playerX + transform.forward * playerZ;

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            controller.Move(playerMove * movingSpeed * Time.deltaTime);
        }

        //rotate camera horizontally
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        head.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //rotate character vertically
        transform.Rotate(Vector3.up * mouseX);

        //jumping
        /*if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
        }*/

        //gravity
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
