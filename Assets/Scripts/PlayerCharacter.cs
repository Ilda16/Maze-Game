using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Import the SceneManager namespace

public class PlayerCharacter : MonoBehaviour
{
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        health = 1;  // Set initial health to 1
        Debug.Log($"Starting Health: {health}");
    }

    // Method to call to deal damage to the player
    public void Hurt(int damage)
    {
        health -= damage;
        Debug.Log($"Health: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    // Method to handle the player's death
    private void Die()
    {
        Debug.Log("Player has died.");

        // Reload the current scene to reset the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}