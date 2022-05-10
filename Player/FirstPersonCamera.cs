using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {
    public float Mouse_Sensitivity = 100;
    public Transform Player_Body;
    float Rotation_X = 0f;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        float Mouse_X = Input.GetAxis("Mouse X") * Mouse_Sensitivity * Time.deltaTime; 
        float Mouse_Y = Input.GetAxis("Mouse Y") * Mouse_Sensitivity * Time.deltaTime; 

        Rotation_X -= Mouse_Y;
        Rotation_X = Mathf.Clamp(Rotation_X, -90f, 90f);

        transform.localRotation = Quaternion.Euler(Rotation_X, 0f, 0f);
        Player_Body.Rotate(Vector3.up * Mouse_X);
    }
}
