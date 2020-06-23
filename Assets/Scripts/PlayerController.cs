using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;
    private PlayerMotor motor;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        //calculate movement velocity as a Vector3
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVirtical = transform.forward * zMov;

        //calculate final movement velocity and deliver it to PlayerMotor
        Vector3 velocity = (movHorizontal + movVirtical).normalized * speed;
        motor.Move(velocity);

        //calculate rotation (turning around)
        float yRot = Input.GetAxisRaw("Mouse X");
        Vector3 rotation = new Vector3(0f, yRot, 0f) * lookSensitivity;

        //apply rotation
        motor.Rotator(rotation);

        //calculate camera rotation (turning around)
        float xRot = Input.GetAxisRaw("Mouse Y");
        Vector3 cameraRotation = new Vector3(xRot, 0f, 0f) * lookSensitivity;

        //apply camera rotation
        motor.RotateCamera(cameraRotation);
    }
}
