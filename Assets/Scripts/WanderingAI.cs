using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour   
{
    // Projectile to shoot
    [SerializeField] private GameObject fireballPrefab;
    private GameObject fireball;

    //Obstacle detection and movement
    public float speed = 3.0f;
    public float obstacleRange = 5.0f;

    // State of the game object, as a bool
    private bool isAlive;
    


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
    
    public void Start() {
        isAlive = true;     // Set the game object to alive
        
    }
    
    // Update is called once per frame
    void Update()   
{
    // Move forward
    if (isAlive)    
    {
        
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    // Create a ray in the same direction as the game object's direction of movement
    Ray ray = new Ray(transform.position, transform.forward);
    // Perform a sphere cast
    RaycastHit hit;
    if (Physics.SphereCast(ray, 0.75f, out hit))    
    {
        // Get a reference to the game object hit by the spherecast
        GameObject hitObject = hit.transform.gameObject;

        // If the object hit was the player, shoot a fireball at the player
        // Otherwise, if the object is within the obstacle range, turn around
        if (hitObject.GetComponent<PlayerCharacter>())  
        {
            if (fireball == null && fireballPrefab != null)   
            {
                fireball = Instantiate(fireballPrefab) as GameObject;
                fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                fireball.transform.rotation = transform.rotation;
            }
            else if (fireballPrefab == null)
            {
                Debug.LogError("Fireball prefab is not assigned!");
            }
        } 
        else if (hit.distance < obstacleRange)  
        {
            float angle = Random.Range(-110, 110);
            transform.Rotate(0, angle, 0);
        }
    }
}

    // Public method to set isAlive
    public void SetAlive(bool alive)   {
        isAlive = alive;
    }
}