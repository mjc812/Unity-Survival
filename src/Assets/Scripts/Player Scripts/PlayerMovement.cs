using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float XSpeed = 5f;
    private float YSpeed = 8f;
    private float gravity = 20f;
    private float jumpForce = 6f;
    private float verticalForce = 0;

    private CharacterController characterController;
    private Vector3 direction;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        float x = XForce();
        float y = YForce();
        float z = ZForce();

        direction = new Vector3(x, y, z);
        direction = transform.TransformDirection(direction);
        direction = direction * Time.deltaTime;
        characterController.Move(direction);
    }

    float XForce()
    {
        return Input.GetAxis("Horizontal") * XSpeed;
    }

    float YForce()
    {
        if (!characterController.isGrounded)
        {
            verticalForce = verticalForce - (gravity * Time.deltaTime);
        }
        else if (characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalForce = jumpForce;
        }
        return verticalForce;
    }
    float ZForce()
    {
        return Input.GetAxis("Vertical") * YSpeed;
    }
}
