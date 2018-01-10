using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public SceneManager scene;
    private void Start()
    {
        
    }

  public void ClickToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ClickToReturn(GameObject menu)
    {
        menu.SetActive(false);
    }

    public void ClickToExit()
    {
        Application.Quit();
    }
}
