using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour {

    [SerializeField] private float levelLoadDelay = 3f;
    [SerializeField] private GameObject playerDeathFX;

    private bool isPlayerDead = false;

    private void Start()
    {
        playerDeathFX.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        StartDeathSequence();
    }

    private void StartDeathSequence() {
        SendMessage(Constants.METHOD_ON_PLAYER_DEATH);
        ReloadLevel();
    }

    private void OnPlayerDeath(){
        if (!isPlayerDead) {
            isPlayerDead = true;
            playerDeathFX.SetActive(true);
            GameObject.Find("PlayerShip").SetActive(false);
        }
    }

    private void ReloadLevel(){
        FindObjectOfType<GameManager>().ReloadLevelAfterDelay(levelLoadDelay);
    }
}
