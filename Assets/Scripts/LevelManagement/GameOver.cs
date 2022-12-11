using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public SpearCounter spearCounter;
    // Start is called before the first frame update
    void Start()
    {

    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Player" && spearCounter.spears >= 2) {
            CompleteGame();
        }
    }

    private void CompleteGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
