using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float cameraRotationLimits = 85f;

    private Vector3 velocity = Vector3.zero;
    private Rigidbody rb;
    private Vector3 rotation = Vector3.zero;
    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;
    private Vector3 thrusterForce = Vector3.zero;

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
    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    //get the thruster force from PlayerController
    public void ApplyThruster(Vector3 _thrusterForce)
    {
        thrusterForce = _thrusterForce;
    }

    void FixedUpdate()
    {
        MovePerformance();
        RotatePerformance();
        
    }

    //Move the position of the Player based on the velocity
    //Add an upward force for the Player to jump based on the thruster force
    private void MovePerformance()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }

        if (thrusterForce != Vector3.zero)
        {
            rb.AddForce(thrusterForce * Time.deltaTime, ForceMode.Acceleration);
        }
    }

    //Rotate the Player based on the rotation
    void RotatePerformance()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null)
        {
            //set the rotation and clamp it
            currentCameraRotationX -= cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimits, cameraRotationLimits);

            //apply the rotation to the Player's camera
            cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        }
    }
}
