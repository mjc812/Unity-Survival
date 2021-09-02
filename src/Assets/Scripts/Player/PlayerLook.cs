using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private Transform playerTransform;
    private Transform cameraHolderTransform;

    private bool invert = false;
    private float sensivity = 5f;
    private float cameraXMin = -90f;
    private float cameraXMax = 90f;

    private float playerY = 0f;
    private float cameraX = 0f;

    void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        cameraHolderTransform = GameObject.FindWithTag("CameraHolder").GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CameraX();
        CameraY();

        playerTransform.localRotation = Quaternion.Euler(0f, playerY, 0f);
        cameraHolderTransform.localRotation = Quaternion.Euler(cameraX, 0f, 0f);
    }

    void CameraX()
    {
        cameraX = cameraX + (Input.GetAxis("Mouse Y") * sensivity * (invert ? 1f : -1f));
        cameraX = Mathf.Clamp(cameraX, cameraXMin, cameraXMax);
    }
    void CameraY()
    {
        playerY = playerY + (Input.GetAxis("Mouse X") * sensivity);        
    }
}
