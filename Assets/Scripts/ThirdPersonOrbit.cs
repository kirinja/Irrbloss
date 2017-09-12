using UnityEngine;

/// <summary>
/// This is a fairly simple camera script that makes the camera orbit around a Target
/// </summary>
public class ThirdPersonOrbit : MonoBehaviour
{
    [Tooltip("The Target the camera will orbit around")]
    private Transform Target;

    [Tooltip("Camera speed horizontally")]
    public float HorizontalSensitivity = 40.0f;
    [Tooltip("Camera speed vertically")]
    public float VerticalSensitivity = 40.0f;
    //[Tooltip("Camera speed horizontally (for gamepad)")]
    //public float HorizontalSensitivityGamepad = 40.0f;
    //[Tooltip("Camera speed vertically (for gamepad)")]
    //public float VerticalSensitivityGamepad = 40.0f;

    [Tooltip("The minimum angle the camera can move vertically, measured in degrees")]
    public float ClampCameraMin = -15f;
    [Tooltip("The maximum angle the camera can move vertically, measured in degrees")]
    public float ClampCameraMax = 40f;

    [Tooltip("The preferred distance of the camera, relative to the Target, in Unity units")]
    public float PreferredDistance = 10.0f;
    

    public bool InvertX;
    public bool InvertY;

    private bool isUsingController;
    private float x;
    private float y;
    private float actualDistance;

    [HideInInspector]
    public float Margin = 0.3f;
    [HideInInspector]
    public float OffsetY = 0.1f;
    

    void Start()
    {
        // Getting the angles to start from
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        actualDistance = PreferredDistance;

        Target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
    }

    void LateUpdate()
    {
        if (!Target) return;

        //if (isUsingController)
        //{
        //    x += Input.GetAxis("Right Horizontal") * HorizontalSensitivityGamepad * Time.deltaTime * (InvertX ? 1 : -1);
        //    y -= Input.GetAxis("Right Vertical") * VerticalSensitivityGamepad * Time.deltaTime * (InvertY ? 1 : -1);
        //}
        //else
        //{
            x += Input.GetAxisRaw("Mouse X") * HorizontalSensitivity * Time.deltaTime * (InvertX ? 1 : -1);
            y -= Input.GetAxisRaw("Mouse Y") * VerticalSensitivity * Time.deltaTime * (InvertY ? 1 : -1);
        //}
        
        y = ClampAngle(y, ClampCameraMin, ClampCameraMax);
        Quaternion rotation = Quaternion.Euler(y, x, 0); // this might seem confusing but that's because yaw and pitch, X plane corresponds to moving UP/DOWN and Y lane corresponds to LEFT/RIGHT
        
        Vector3 negDistance = new Vector3(0.0f, OffsetY, -actualDistance);
        Vector3 position = rotation * negDistance + Target.position;

        transform.rotation = rotation;
        transform.position = position;
    }


    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}