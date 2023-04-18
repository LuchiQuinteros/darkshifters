using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public static SceneLoader sceneLoader;
    public GameObject mainButtons;
    public GameObject mainOptions;
    public GameObject aboutText;
    public GameObject backButton;

    private void Start()
    {
        sceneLoader = this;
    }
    
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Options()
    {
        print("Coming soon...");
        mainButtons.SetActive(false);
        mainOptions.SetActive(true);
        backButton.SetActive(true);
    }
    public void About()
    {
        mainButtons.SetActive(false);
        aboutText.SetActive(true);
        backButton.SetActive(true);
    }

    public void Back()
    {
        mainButtons.SetActive(true);
        mainOptions.SetActive(false);
        aboutText.SetActive(false);
        backButton.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}