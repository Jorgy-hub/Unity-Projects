using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour {
    public float Amount;
    public float Smooth_Amount;
    public Vector3 Initial_Position;

    void Start() {
        Initial_Position = transform.localPosition;
    }

    void Update() {
        float Movement_X = -Input.GetAxis("Mouse X") * Amount;
        float Movement_Y = -Input.GetAxis("Mouse Y") * Amount;

        Vector3 Final_Position = new Vector3(Movement_X, Movement_Y, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, Final_Position + Initial_Position, Time.deltaTime * Smooth_Amount);
    }
}
