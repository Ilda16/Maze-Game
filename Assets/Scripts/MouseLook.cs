using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour  {

    // Enum to define different mouse control modes
    public enum RotationAxes    {
        MouseXandY = 0,
        MouseX = 1,
        MouseY = 2
    }

    // Public variables for rotation axes, sensitivity, and rotation limits
    public RotationAxes axes = RotationAxes.MouseXandY;
    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;

    // Minimum vertical rotation limit
    public float minimumVert = -45f;
    // Maximum vertical rotation limit
    public float maximumVert = 45f;

    // track of the vertical rotation
    private float verticalRot = 0;

    private void start()    {
        // Prevent Rigidbody from rotating due to physics interactions
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null)  {
            rigidbody.freezeRotation = true;
        }
    }


    // Update is called once per frame
    void Update()   {
        if (axes == RotationAxes.MouseX){
            // Horizontal rotation
            transform.Rotate(0, sensitivityHor * Input.GetAxis("Mouse X"), 0);
        } else if (axes == RotationAxes.MouseY) {
            // Vertical rotation
            verticalRot -= Input.GetAxis("Mouse Y") * sensitivityVert;
            verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);

            // Get current horizontal rotation to keep it constant while adjusting vertical rotation
            float horizontalRot = transform.localEulerAngles.y;

            // Apply the new rotation (vertical + horizontal) to the transform
            transform.localEulerAngles = new Vector3(verticalRot, horizontalRot, 0);

        } else {
            //Horizontal and Vertical rotation
            verticalRot -= Input.GetAxis("Mouse Y") * sensitivityVert;
            verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);

            // Get the change for horizontal mouse movement
            float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            float horizontalRot = transform.localEulerAngles.y + delta;

            // Apply the new rotation vertical + horizontal to the transform
            transform.localEulerAngles = new Vector3(verticalRot, horizontalRot, 0);

        }
        
    }
}
