using UnityEngine;
using UnityEngine.SceneManagement; // WICHTIG: Das brauchen wir zum Szenen wechseln!

public class MenuController : MonoBehaviour
{
    // Funktion für den "Play" Button im Homescreen
    public void PlayGame()
    {
        // Lädt das erste Level (Szene 1 in den Build Settings)
        SceneManager.LoadScene(1); 
    }

    // Funktion für den "Retry" Button im Deathscreen
    public void RetryGame()
    {
        // Lädt die exakt gleiche Szene noch einmal neu
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Funktion für beide "Quit" Buttons
    public void QuitGame()
    {
        Debug.Log("Spiel wird beendet!"); // Zeigt uns im Editor, dass es funktioniert
        Application.Quit(); // Schließt das fertige Spiel
    }
}