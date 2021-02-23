using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public PlayerMusicStream musicStream;

    public GameObject gameOverScreen, duringGameScreen;

    // Start is called before the first frame update
    void Start()
    {
        musicStream.TooManyMistakes += OnGameOver;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGameOver() {
        gameOverScreen.SetActive(true);
        duringGameScreen.SetActive(false);
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
