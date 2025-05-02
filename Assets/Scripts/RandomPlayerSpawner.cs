using UnityEngine;

public class RandomPlayerSpawner : MonoBehaviour
{
    public GameObject player;               // Assign your player object (not a prefab) in the inspector
    public string floorTag = "Floor";        // Tag used on all your floor tiles
    public float checkRadius = 3.0f;         // Radius to check for obstacles

    void Start()
    {
        MovePlayerRandomly();
    }

    void MovePlayerRandomly()
    {
        if (player == null)
        {
            Debug.LogWarning("Player object not assigned!");
            return;
        }

        GameObject[] floorTiles = GameObject.FindGameObjectsWithTag(floorTag);

        if (floorTiles.Length == 0)
        {
            Debug.LogWarning("No floor tiles found with the 'Floor' tag!");
            return;
        }

        Vector3 randomPosition = Vector3.zero;
        bool positionIsValid = false;
        int attempts = 0;

        while (!positionIsValid && attempts < 10)
        {
            // Pick a random floor
            GameObject floorTile = floorTiles[Random.Range(0, floorTiles.Length)];
            Collider floorCollider = floorTile.GetComponent<Collider>();

            if (floorCollider == null)
            {
                Debug.LogWarning("One of the floor tiles is missing a collider!");
                continue;
            }

            // Generate a random position within that floor's bounds
            randomPosition = new Vector3(
                Random.Range(floorCollider.bounds.min.x, floorCollider.bounds.max.x),
                floorTile.transform.position.y, // Floor height
                Random.Range(floorCollider.bounds.min.z, floorCollider.bounds.max.z)
            );

            // Check for obstacles at that spot
            if (!Physics.CheckSphere(randomPosition, checkRadius))
            {
                positionIsValid = true;
            }

            attempts++;
        }

        if (positionIsValid)
        {
            player.transform.position = randomPosition;
        }
        else
        {
            Debug.LogWarning("Couldn't find a valid spawn location after several attempts.");
        }
    }
}
