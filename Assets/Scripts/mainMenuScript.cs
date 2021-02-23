using UnityEngine.SceneManagement;
using UnityEngine;

public class mainMenuScript : MonoBehaviour
{
    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
    
