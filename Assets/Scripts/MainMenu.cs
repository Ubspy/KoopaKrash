using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{ 
    public void startSim()
    {
        SceneManager.LoadSceneAsync("Scenes/Koopa Krash");
    }

    public void exitSim()
    {
        Application.Quit();
    }
}
