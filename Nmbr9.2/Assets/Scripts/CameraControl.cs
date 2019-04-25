using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //https://gamedev.stackexchange.com/questions/104693/how-to-use-input-getaxismouse-x-y-to-rotate-the-camera

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        pitch = Mathf.Clamp(pitch, -85.0f, 0.0f);
        yaw = Mathf.Clamp(yaw, -80.0f, 80.0f);

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        //transform.rotation += new Vector3(pitch, yaw,)
    }
}
