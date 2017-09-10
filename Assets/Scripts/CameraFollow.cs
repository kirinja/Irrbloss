using UnityEngine;

public enum CameraDirection
{
    Left,
    Right,
    Front,
    Back

};

public class CameraFollow : MonoBehaviour
{
    private Transform _player;

    public Vector3 CameraOffset;
	// Use this for initialization
	void Start ()
	{
	    _player = GameObject.FindGameObjectWithTag("Player").transform;
        CameraOffset = new Vector3(0, 0, 10);
        //transform.LookAt(_player);
	}
	
	// Update is called once per frame
	void Update ()
	{
        /* // testing camera angles
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeAngle(CameraDirection.Front);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeAngle(CameraDirection.Back);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            ChangeAngle(CameraDirection.Left);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            ChangeAngle(CameraDirection.Right);
        */

        transform.position = _player.position - CameraOffset;
	}

    // can add more angles later if we want
    public void ChangeAngle(CameraDirection direction)
    {
        switch (direction)
        {
            case CameraDirection.Left:
                // left is 90 rotation and 10, 0, 0
                transform.localRotation = Quaternion.Euler(0, 90, 0);
                CameraOffset = new Vector3(10, 0, 0);
                break;
            case CameraDirection.Right:
                // right is 270 rotation and -10, 0, 0
                transform.localRotation = Quaternion.Euler(0, 270, 0);
                CameraOffset = new Vector3(-10, 0, 0);
                break;
            case CameraDirection.Front:
                // front is 0 rotation and 0, 0, 10
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                CameraOffset = new Vector3(0, 0, 10);
                break;
            case CameraDirection.Back:
                // back is 180 rotation and 0, 0, -10
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                CameraOffset = new Vector3(0, 0, -10);
                break;
        }
    }
}
