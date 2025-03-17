using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour    {

    // What prefab to spawn
    [SerializeField] GameObject enemyPrefab;

    // Private field to track a single instance of the enemy
    private GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()   {

        // If there isn't an enemy, spawn one
        if (enemy == null)  {
            enemy = Instantiate(enemyPrefab) as GameObject;
            enemy.transform.position = new Vector3(0, 1, 0);
            float angle = Random.Range(0, 360);
            enemy.transform.Rotate(0, angle, 0);
        }
    }
}
