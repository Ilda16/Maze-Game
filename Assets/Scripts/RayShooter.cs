using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayShooter : MonoBehaviour
{
    // Private field; stores a reference to the camera
    private Camera cam;

    // Cooldown duration in seconds (10 seconds)
    public float cooldownTime = 10f;
    private float timeSinceLastShot = 0f;
    private bool canShoot = true;  // Flag to indicate whether the player can shoot

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
         if (cam == null)
    {
        Debug.LogError("Camera component not found on the GameObject.");
    }

        // Hide the cursor
        // Cursor.lockState =  CursorLockMode.Locked;
        // Cursor.visible = false;
    }

    // OnGUI method; for drawing a crosshair
    private void OnGUI()
    {
        int size = 24;

        float posX = cam.pixelWidth / 2 - size / 4;
        float posY = cam.pixelHeight / 2 - size / 2;

        GUI.Label(new Rect(posX, posY, size, size), "+");

        if (GUI.Button(new Rect(10, 10, 180, 20), "FIND THE EXIT!"))
        {
            Debug.Log("Button has been clicked!");
        }
    }

    // Coroutine: Place down a sphere at a location, which then disappears after one second
    private IEnumerator SphereIndicator(Vector3 pos)
    {
        // Create a new sphere game object
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        // Place sphere at pos passed in
        sphere.transform.position = pos;

        // Wait one second
        yield return new WaitForSeconds(1);

        // Destroy the sphere
        Destroy(sphere);
    }

    // Update is called once per frame
    void Update()
    {
        // If enough time has passed since the last shot, increment the cooldown timer
        if (!canShoot)
        {
            timeSinceLastShot += Time.deltaTime;

            // If cooldown time has passed, reset the flag to allow shooting again
            if (timeSinceLastShot >= cooldownTime)
            {
                canShoot = true;
            }
        }

        // When a player left-clicks and cooldown has passed, perform a raycast
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && canShoot)
        {
            // Reset the cooldown timer
            canShoot = false;
            timeSinceLastShot = 0f;

            // Calculate the center of the screen
            Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0);

            // Create a ray whose starting point is the middle of the screen
            Ray ray = cam.ScreenPointToRay(point);

            // Create a raycast object to figure out what was hit
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Print out the coordinates of where the ray hit
                Debug.Log("Hit: " + hit.point);

                // If object hit was a reactive target, say that it was hit
                // Otherwise, place down a sphere
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

                if (target != null)
                {
                    Debug.Log("Target hit!");

                    // Call ReactToHit on the target
                    target.ReactToHit();
                }
                else
                {
                    StartCoroutine(SphereIndicator(hit.point));
                }
            }
        }
    }
}













