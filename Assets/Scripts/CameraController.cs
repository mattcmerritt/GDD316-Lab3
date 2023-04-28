using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float MoveSpeed, SprintMoveSpeed;
    protected void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.LeftShift)) { }
        transform.position += input * (Input.GetKey(KeyCode.LeftShift) ? SprintMoveSpeed : MoveSpeed) * Time.deltaTime;
    }
}
