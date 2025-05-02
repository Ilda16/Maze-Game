using UnityEngine;

public class RandomPlayerSpawner : MonoBehaviour
{
    public GameObject player;               // Assign your player object (not a prefab) in the inspector
   

     //array of the spawn locations
    public GameObject[] spawnLocations; // Assign your spawn locations in the inspector
    
    void MovePlayerRandomly()// Reference to the MovePlayer script
    {
        // Check if the player and spawn locations are assigned
        if (player == null || spawnLocations.Length == 0)
        {
            Debug.LogError("Player or spawn locations not assigned.");
            return;
        }

        // Get a random index from the spawn locations array
        int randomIndex = Random.Range(0, spawnLocations.Length);

        // Get the position of the randomly selected spawn location
        Vector3 randomPosition = spawnLocations[randomIndex].transform.position;

        // Move the player to the random position
        player.transform.position = randomPosition;
    }
    void Start()
    {
        MovePlayerRandomly();
    }

}
