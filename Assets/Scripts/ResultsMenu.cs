using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class ResultsMenu : MonoBehaviour
{
    // Text objects
    public TextMeshProUGUI greenMass, greenInitialVelocity, greenFinalVelocity, redMass, redInitialVelocity, redFinalVelocity;

    private void OnEnable()
    {
        Time.timeScale = 0f;
    }

    public void setResults(float gMass, float gInitVelocity, float gFinalVelocity, float rMass, float rInitVelocity, float rFinalVelocity)
    {
        // Sets green results
        greenMass.text = "Green mass: " + (gMass + "kg");
        greenInitialVelocity.text = "Green initial velocity: " + (gInitVelocity + "m/s");
        greenFinalVelocity.text = "Green final velocity:\n" + (Math.Round(gFinalVelocity, 2) + "m/s");

        // Sets red results
        redMass.text = "Red mass: " + (rMass + "kg");
        redInitialVelocity.text = "Red initial velocity: " + (rInitVelocity + "m/s");
        redFinalVelocity.text = "Red final velocity:\n" + (Math.Round(rFinalVelocity, 2) + "m/s");
    }

    public void restartSim()
    {
        SceneManager.LoadSceneAsync("Scenes/Koopa Krash");
    }

    public void exitSim()
    {
        // Resets time scale so physics and animations work again
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("Scenes/Main Menu");
    }
}
