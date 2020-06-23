using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Rigidbody rb;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //get the movement velocity from PlayController
    public void Move (Vector3 _velocity)
    {
        velocity = _velocity;
    }

    //get the rotation from PlayController
    public void Rotator(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    //get the cameraRotation from PlayController
    public void RotateCamera(Vector3 _cameraRotation)
    {
        cameraRotation = _cameraRotation;
    }

    void FixedUpdate()
    {
        MovePerformance();
        RotatePerformance();
    }

    //Move the position of the Player based on the velocity
    private void MovePerformance()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    //Rotate the Player based on the rotation
    private void RotatePerformance()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null)
        {
            cam.transform.Rotate(-cameraRotation);
        }
    }

}
