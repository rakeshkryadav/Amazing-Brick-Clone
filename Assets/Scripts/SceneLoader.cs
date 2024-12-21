using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadPlayScene(){
        Debug.Log("Loading...");
        SceneManager.LoadScene("PlayScene");
    }

    public void LaodMenuScene(){
        Debug.Log("Loading...");
        SceneManager.LoadScene("MainScene");
    }
}
