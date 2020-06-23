using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;
    [SerializeField]
    private float thrusterForce = 1000f;

    [Header("String settings")]
    [SerializeField]
    private JointProjectionMode jointMode = JointProjectionMode.PositionAndRotation;
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 40f;

    private PlayerMotor motor;
    private ConfigurableJoint joint;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();
        setJontSettings(jointSpring);
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
        float cameraRotationX = xRot * lookSensitivity;

        //apply camera rotation
        motor.RotateCamera(cameraRotationX);

        //calculate the thruster force as a Vector3
        //set the JointSpring to zero if the Player is jumping
        Vector3 _thrusterForce = Vector3.zero;
        if (Input.GetButton("Jump"))
        {
            _thrusterForce = Vector3.up * thrusterForce;
            setJontSettings(0f);
        } else
        {
            setJontSettings(jointSpring);
        }

        //apply the thruster force
        motor.ApplyThruster(_thrusterForce);

    }

    //set the Congigurable Joint settings using the parameters
    private void setJontSettings(float _jointSpring)
    {
        //here is a special syntax
        joint.yDrive = new JointDrive { positionSpring = _jointSpring
            , maximumForce = jointMaxForce };

        joint.projectionMode = jointMode;
    }
}
