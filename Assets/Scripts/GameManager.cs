using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private void Awake() {
        int numOfGameManagers = FindObjectsOfType<GameManager>().Length;
        if (numOfGameManagers > 1) {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    private void Start () {
        if (SceneManager.GetActiveScene().buildIndex == 0) {
            Invoke("LoadNextLevel", 5);
        }

	}
	
    /// <summary>
    /// Loads the next indexed level
    /// </summary>
    private void LoadNextLevel() {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        Debug.Log("Loading level: " + nextLevelIndex);
        SceneManager.LoadScene(nextLevelIndex);
    }

    /// <summary>
    /// Reloads the current level after the specified delay in seconds
    /// </summary>
    /// <param name="delay">delay in seconds</param>
    public void ReloadLevelAfterDelay(float delay){
        Invoke("ReloadLevel", delay);
    }

    /// <summary>
    /// Reloads the current level
    /// </summary>
    private void ReloadLevel(){
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
