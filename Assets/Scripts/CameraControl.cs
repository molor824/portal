using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] float _speed = 10;
    [SerializeField] float _sensitivity = 2;

    Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        var vel = new Vector3();

        if (Input.GetKey(KeyCode.W)) vel.z++;
        if (Input.GetKey(KeyCode.S)) vel.z--;
        if (Input.GetKey(KeyCode.D)) vel.x++;
        if (Input.GetKey(KeyCode.A)) vel.x--;
        if (Input.GetKey(KeyCode.E)) vel.y++;
        if (Input.GetKey(KeyCode.Q)) vel.y--;

        _rb.velocity = transform.rotation * vel * _speed;

        if (Input.GetKey(KeyCode.Mouse1)) Cursor.lockState = CursorLockMode.Locked;
        else
        {
            Cursor.lockState = CursorLockMode.None;
            return;
        }

        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * _sensitivity, 0), Space.World);

        var rot = transform.localEulerAngles;
        rot.x -= Input.GetAxis("Mouse Y") * _sensitivity;
        if (rot.x > 90 && rot.x < 270)
        {
            if (rot.x > 180) rot.x = 270;
            else rot.x = 90;
        }

        transform.localEulerAngles = rot;
    }
}
