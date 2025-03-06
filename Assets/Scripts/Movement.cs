using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour {

    public float speed = 3f;
    public float gravity = -9.8f;

    private CharacterController charController;

    public const float _baseSpeed = 3f;

    private void OnEnable() {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    private void OnDisable()    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }


    private void OnSpeedChanged(float value)    {
        speed = _baseSpeed * value;
    }

    // Start is called before the first frame update
    void Start()    {

        charController = GetComponent<CharacterController>();  

    }

    // Update is called once per frame
    void Update()   {
        
        // Get horizontal and vertical movement from player's keyboard input
        float deltaX = Input.GetAxis("Horizontal") * speed /** Time.deltaTime*/;
        float deltaZ = Input.GetAxis("Vertical") * speed /** Time.deltaTime*/;

        // Creates a new vector for this movement
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);

        // Clamp magnitutde so it moves no faster than the speed
        movement = Vector3.ClampMagnitude(movement, speed);

        // Applies gravity
        movement.y = gravity;

        // Multiply by time.deltatime so movement is agnostic of framerate
        movement *= Time.deltaTime;

        // Transform from local coords to global coords
        movement = transform.TransformDirection(movement);

        // Call the character controller's move method and pass in the movemment vector
        charController.Move(movement);   
    }
}
