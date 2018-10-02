using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class SettingsMenu : MonoBehaviour
{
    // Stores ellastic property of collision
    // The 'bool?' makes it a nullable bool
    private bool? isElastic;

    // Public button and sprite objects
    public Button elasticButton, inelasticButton;
    public Sprite activeImage, inactiveImage;

    // Input fields
    public TMP_InputField greenVelocity, greenMass, redVelocity, redMass;

    // Error text field
    public TextMeshProUGUI errorText;

    // Koopa Controller object
    public KoopaControl koopaControl;

    private void OnEnable()
    {
        // Once this menu is enabled, time is stopped
        Time.timeScale = 0f;

        // Sets default values for input boxes
        greenVelocity.text = "5";
        greenMass.text = "2";
        redVelocity.text = "-5";
        redMass.text = "2";
    }

    public void setEllastic()
    {
        // Toggles images and bool
        elasticButton.image.sprite = activeImage;
        inelasticButton.image.sprite = inactiveImage;
        isElastic = true;
    }

    public void setInellastic()
    {
        // Toggles images and bool
        elasticButton.image.sprite = inactiveImage;
        inelasticButton.image.sprite = activeImage;
        isElastic = false;
    }

    public void setKoopaData()
    {
        // Gotta check for this first, or else the game will go on even if the velocities are the same due to else behavior
        if(float.Parse(greenVelocity.text) == float.Parse(redVelocity.text))
        {
            errorText.text = "Both of your velocities are the same, they will never colide. Try changing one to negative";
        }
        else if(checkNull())
        {
            // Time is set back to normal, and this menu is disabled
            Time.timeScale = 1f;
            gameObject.SetActive(false);

            // Sets values for physics
            koopaControl.setKoopaValues(float.Parse(greenVelocity.text), float.Parse(greenMass.text), float.Parse(redVelocity.text), float.Parse(redMass.text), isElastic.Value);
        }
        else
        {
            errorText.text = getErrorString();
        }
    }

    private bool checkNull()
    {
        // Boolean vars for null objects
        bool greenVelocityNull = (greenVelocity.text == "");
        bool greenMassNull = (greenMass.text == "");
        bool redVelocityNull = (redVelocity.text == "");
        bool redMassNull = (redMass.text == "");

        // If any input is null, return false
        if(greenVelocityNull || greenMassNull || redVelocityNull || redMassNull || (!isElastic.HasValue))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private string getErrorString()
    {
        string error = "Must set value of: ";

        if(greenVelocity.text == "")
        {
            error += "green velocity, ";
        }

        if(greenMass.text == "")
        {
            error += "green mass, ";
        }

        if(redVelocity.text == "")
        {
            error += "red velocity, ";
        }

        if(redMass.text == "")
        {
            error += "red mass, ";
        }

        if(!isElastic.HasValue)
        {
            error += "collision type, ";
        }

        // After arguments are added, remove final ', '
        error = error.Substring(0, error.Length - 2);

        return error;
    }

    public void checkGreenVelocity()
    {
        float velocity;
        bool isNumeric = float.TryParse(greenVelocity.text, out velocity);

        // If the text box doesn't have a number
        if (!isNumeric)
        {
            greenVelocity.text = "";
        }
    }

    public void checkRedVelocity()
    {
        float velocity;
        bool isNumeric = float.TryParse(redVelocity.text, out velocity);

        if (!isNumeric)
        {
            redVelocity.text = "";
        }
    }

    public void checkGreenMass()
    {
        float mass;
        bool isNumeric = float.TryParse(greenMass.text, out mass);

        if (!isNumeric)
        {
            greenMass.text = "";
        }
        else
        {
            // If it's numeric, it has to be positive
            if (mass < 0)
            {
                // If it's not positive, change it to positive
                greenMass.text = Math.Abs(mass).ToString();
            }
        }
    }

    public void checkRedMass()
    {
        float mass;
        bool isNumeric = float.TryParse(redMass.text, out mass);

        if (!isNumeric)
        {
            redMass.text = "";
        }
        else
        {
            // If it's numeric, it has to be positive
            if (mass < 0)
            {
                // If it's not positive, change it to positive
                redMass.text = Math.Abs(mass).ToString();
            }
        }
    }
}
