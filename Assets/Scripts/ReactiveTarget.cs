using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour {

    public Coroutine deathAnim { private set; get; }
    private WanderingAI behavior;

    // Start is called before the first frame update
    void Start() {
        behavior = GetComponent<WanderingAI>();
    }

    // Stun coroutine: Disables the enemy AI for 5 seconds
    public IEnumerator Stun() {
        Debug.Log("Stun coroutine started");

        // Disable AI behavior (stun)
        if (behavior != null) {
            behavior.SetAlive(false); // Assuming this method stops AI movement
            Debug.Log("AI is stunned");
        }

        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        // Re-enable AI behavior after the stun time
        if (behavior != null) {
            behavior.SetAlive(true); // Assuming this method restarts AI movement
            Debug.Log("AI is unstunned");
        }
    }

    // React to being hit (trigger the stun)
    public void ReactToHit() {
        Debug.Log("ReactToHit() triggered");

        // Only start the stun if it's not already happening
        if (deathAnim == null) {
            deathAnim = StartCoroutine(Stun());  // Start the stun coroutine
        }
    }
}
