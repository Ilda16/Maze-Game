using UnityEngine;
 using UnityEngine.UI;
 using UnityEngine.SceneManagement; // Needed for scene management
 using System.Collections;
 using TMPro;
 
 public class VictoryTrigger : MonoBehaviour
 {
     // Reference to the UI elements
    // public GameObject victoryPanel;  // The panel that will be displayed on victory
     public TMP_Text victoryText;         // Text component to display the victory message
     public string victoryMessage = "Victory";  // Default victory message
 
     // Time delay in seconds before restarting the game
     public float restartDelay = 10f;
 
     // This function will be called when the player collides with the exit door
     private void OnTriggerEnter(Collider other)
     {
         // Check if the object colliding is the player
         if (other.CompareTag("Player"))  // Ensure your player has the "Player" tag
         {
             ShowVictoryPopup();
         }
     }
 
     // Function to display the victory popup
     private void ShowVictoryPopup()
     {
         // Enable the victory panel
        // if (victoryPanel != null)
        // {
            // victoryPanel.SetActive(true);
        // }
 
         // Set the victory message
         if (victoryText != null)
         {
             victoryText.gameObject.SetActive(true); 
             victoryText.text = victoryMessage;
         }
 
         // Start the coroutine to reset the game after a delay
         StartCoroutine(WaitAndRestart());
     }
 
     // Coroutine that waits for the specified delay and then resets the scene
     private IEnumerator WaitAndRestart()
     {
         // Wait for the specified number of seconds (restartDelay)
         yield return new WaitForSeconds(restartDelay);
 
         // Reset the game (reload the current scene)
         RestartGame();
     }
 
     // Function to restart the game (reload the current scene)
     private void RestartGame()
     {
         // Get the current scene's name and reload it
         Scene currentScene = SceneManager.GetActiveScene();
         SceneManager.LoadScene(currentScene.name);
     }
 }
