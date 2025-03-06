using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour    {

    private int health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Method to call to deal damage to the player
    public void Hurt(int damage)    {
        health -= damage;
        Debug.Log($"Health: {health}");
    }
}
