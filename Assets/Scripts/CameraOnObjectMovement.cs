using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOnObjectMovement : MonoBehaviour
{
    public float MOUSE_SENSITIVITY = 0.1f;
    public float MAX_COS = 0.4f;
    public float MIN_COS = 0.1f;
    public int CAMERA_RADIUS = 10;

    GameObject mainCamera;
    
    readonly string CAMERA_NAME = "MainCamera";

    float oldX;
    float oldY;

    void Start()
    {
        oldX = 0f;
        oldY = Mathf.Acos((MIN_COS + MAX_COS) / 2f);
        mainCamera = GameObject.Find(CAMERA_NAME);
    }

    void Update()
    {
        float mouseX = MOUSE_SENSITIVITY * -Input.GetAxis("Mouse X");
        float mouseY = MOUSE_SENSITIVITY * Input.GetAxis("Mouse Y");
        UpdateCameraPosition(mouseX, mouseY);

    }

    void UpdateCameraPosition(float mouseX, float mouseY)
    {
        float cosA;
        float cosB;

        float sinA;
        float sinB;

        if (Mathf.Cos(mouseY + oldY) > MAX_COS || Mathf.Cos(mouseY + oldY) < MIN_COS)
        {
            mouseY = 0;
        }

        cosA = Mathf.Cos(mouseX + oldX);
        cosB = Mathf.Cos(mouseY + oldY);

        sinA = Mathf.Sin(mouseX + oldX);
        sinB = Mathf.Sin(mouseY + oldY);

        float x = CAMERA_RADIUS * cosA * sinB;
        float y = CAMERA_RADIUS * cosB;
        float z = CAMERA_RADIUS * sinA * sinB;

        mainCamera.transform.position = gameObject.transform.position + new Vector3(x, y, z);
        mainCamera.transform.LookAt(gameObject.transform);

        oldX += mouseX;
        oldY += mouseY;
    }

    public void SetCameraLookDirection(float angle)
    {
        oldX = -Mathf.Deg2Rad * (angle + 90);
    }

}
