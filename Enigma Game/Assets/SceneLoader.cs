using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance { get; private set; }

    private void Awake()
    {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadNextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 >= SceneManager.sceneCount)
            LoadMenu();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
