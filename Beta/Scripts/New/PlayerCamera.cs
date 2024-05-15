using UnityEngine;
using System.Runtime.CompilerServices;

public class PlayerCamera : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    bool touchFieldTrigger;

    private void Start()
    {
        touchFieldTrigger = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && touchFieldTrigger == true)
        {

            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            yRotation += mouseX;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void TouchFieldTriggerEnter()
    {
        touchFieldTrigger = true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void TouchFieldTriggerDown()
    {
        touchFieldTrigger = false;
    }

}
