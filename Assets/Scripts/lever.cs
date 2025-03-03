using UnityEngine;
using System.Collections;

//The player will interact with the lever to open the door, and 
//the door will close after a certain amount of time
//The door will open when the player presses the E key

public class lever : MonoBehaviour
{
    //instances of the door and the lever
    public GameObject door; // Reference to the door object
    public float openHeight = 3f; // How high the door should move
    public float openSpeed = 2f; // Speed of the door opening
    public float timer = 5f; //how long the door will stay open
    public bool doorOpen = false;//tracks if the door is open or closed
    //-----Public---------//
    private Vector3 doorOpenPos;
    private Vector3 doorClosedPos;
    

void Start()
    {

        // Store the initial position of the door

        if (door != null)
        {
            doorClosedPos = door.transform.position;
        
            doorOpenPos = doorClosedPos + Vector3.up * openHeight;
        }
        
    }

    void Update()
    {
        // Smoothly move the door to its target position
        if (door != null)
        {
            if(doorOpen)
            {
                door.transform.position = Vector3.Lerp(door.transform.position, doorOpenPos, openSpeed * Time.deltaTime);
            }
             else
            {
            door.transform.position = Vector3.Lerp(door.transform.position, doorClosedPos, openSpeed * Time.deltaTime);
            }

        }
       
    }

    //The CloseDoor coroutine waits for the specified timer 
    // duration before setting doorOpen to false.
    void OnTriggerSaty(Collider col)
    {
        //It will check if player is in the trigger Zone and presses the E key
        if (col.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            doorOpen = true;
            Debug.Log("Door opened");
            StartCoroutine(CloseDoor());
        }
    }



   IEnumerator CloseDoor ()
   {
    //wait to run the next line till the other code is implemented
    yield return new WaitForSeconds (timer);
    doorOpen = false;
    Debug.Log ("Door Closed");
   }
}
