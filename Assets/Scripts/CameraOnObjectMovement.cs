using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOnObjectMovement : MonoBehaviour
{
    public float mouseSensitivity = 0.1f;
    public float maxCos = 0.4f;
    public float minCos = 0.1f;
    public int cameraRadius = 10;

    GameObject mainCamera;

    float oldX;
    float oldY;

    void Start()
    {
        oldX = 0f;
        oldY = Mathf.Acos((minCos + maxCos) / 2f);
        mainCamera = GameObject.Find(Config.MAIN_CAMERA_NAME);
    }

    void Update()
    {
        float mouseX = mouseSensitivity * -Input.GetAxis("Mouse X");
        float mouseY = mouseSensitivity * Input.GetAxis("Mouse Y");
        UpdateCameraPosition(mouseX, mouseY);

    }

    void UpdateCameraPosition(float mouseX, float mouseY)
    {
        float cosA;
        float cosB;

        float sinA;
        float sinB;

        if (Mathf.Cos(mouseY + oldY) > maxCos || Mathf.Cos(mouseY + oldY) < minCos)
        {
            mouseY = 0;
        }

        cosA = Mathf.Cos(mouseX + oldX);
        cosB = Mathf.Cos(mouseY + oldY);

        sinA = Mathf.Sin(mouseX + oldX);
        sinB = Mathf.Sin(mouseY + oldY);

        float x = cameraRadius * cosA * sinB;
        float y = cameraRadius * cosB;
        float z = cameraRadius * sinA * sinB;

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
