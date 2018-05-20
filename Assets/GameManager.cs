using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private int currentLevel = 0;

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start () {
        Debug.Log("Current Level: " + currentLevel);
        if (currentLevel == 0) {
            Invoke("LoadNextLevel", 5);
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void LoadNextLevel() {
        currentLevel = currentLevel + 1;
        Debug.Log("Loading level: "+currentLevel);
        SceneManager.LoadScene(currentLevel);
    }
}
