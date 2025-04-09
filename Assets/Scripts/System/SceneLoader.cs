using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneLoader : MonoBehaviour
{
    // This method is used to load a scene by its name.
    // It can be called from other scripts or UI elements.
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
