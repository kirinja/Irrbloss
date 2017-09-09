using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO
// maybe change so the up vector of the transform is perpindicular to the floor
// might remove the weird walking on slopes
[RequireComponent(typeof(Rigidbody))]
public class Controller3D : MonoBehaviour
{
    private Vector3 _velocity;

    public float MaxSpeed = 5.0f;
    public float TurnSmoothing = 15f; // A smoothing value for turning the player.

    // Use this for initialization
    void Start ()
	{
	    _velocity = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // we need to translate the input vector to depend on how the camera is rotated
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        

        _velocity = input * MaxSpeed;
        MovementManagement(input);
        //Move();
        transform.GetComponent<Rigidbody>().position = transform.position;
    }

    void Move(Vector3 input)
    {
        var cameraRelative = Camera.main.transform.TransformDirection(0, 0, -1);
        transform.position -= new Vector3((cameraRelative.x * _velocity.z), 0, (cameraRelative.z * _velocity.z)) * Time.deltaTime;
        cameraRelative = Camera.main.transform.TransformDirection(-1, 0, 0);

        transform.position -= new Vector3((cameraRelative.x * _velocity.x), 0, (cameraRelative.z * _velocity.x)) * Time.deltaTime;
    }

    void MovementManagement(Vector3 input)
    {
        // If there is some axis input...
        if (input.x != 0f || input.z != 0f)
        {
            // ... set the players rotation and set the speed parameter to 5.3f.
            Rotating(input);
            Move(input);
        }
    }

    void Rotating(Vector3 input)
    {
        // Create a new vector of the horizontal and vertical inputs.
        Vector3 targetDirection = input;
        targetDirection = Camera.main.transform.TransformDirection(targetDirection);
        targetDirection.y = 0.0f;

        // Create a rotation based on this new vector assuming that up is the global y axis.
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        // Create a rotation that is an increment closer to the target rotation from the player's rotation.
        Quaternion newRotation = Quaternion.Lerp(transform.GetComponent<Rigidbody>().rotation, targetRotation, TurnSmoothing * Time.deltaTime);

        // Change the players rotation to this new rotation.
        transform.GetComponent<Rigidbody>().MoveRotation(newRotation);
    }
}
