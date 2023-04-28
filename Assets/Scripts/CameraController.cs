using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float MoveSpeed, SprintMoveSpeed;
    [SerializeField, Range(0f, 300f)] private float TurnSpeed;
    protected void Update()
    {
        // translation of the camera
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        transform.position += input * (Input.GetKey(KeyCode.LeftShift) ? SprintMoveSpeed : MoveSpeed) * Time.deltaTime;

        // rotation of the camera
        // float angleInput = (Input.GetKey(KeyCode.E) ? 1 : 0) + (Input.GetKey(KeyCode.Q) ? -1 : 0);
        // transform.eulerAngles += Vector3.up * angleInput * TurnSpeed * Time.deltaTime;  
    }
}
