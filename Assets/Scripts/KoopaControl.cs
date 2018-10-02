using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaControl : MonoBehaviour
{
    // Private speed and mass values
    private float _greenVelocity, _greenMass, _redVelocity, _redMass, _speedFactor;

    // Private collision type
    private bool _isElastic;

    // Koopa object
    public GameObject greenKoopa, redKoopa;

    // Green koopa script
    public GreenKoopa greenKoopaScript;

    // Results menu object
    public ResultsMenu resultsMenu;

    // Private ridigbody objects
    private Rigidbody2D greenRB, redRB;

    public void setKoopaValues(float greenVelocity, float greenMass, float redVelocity, float redMass, bool isElastic)
    {
        _greenVelocity = greenVelocity;
        _greenMass = greenMass;
        _redVelocity = redVelocity;
        _redMass = redMass;
        _isElastic = isElastic;

        // Sends elastic val to green koopa
        greenKoopaScript.setCollisionType(_isElastic);

        initPhysics();
    }

    public void collide()
    {
        // If it's set to an ellastic collision
        if (_isElastic)
        {
            collideElastically();
        }
        else
        {
            collideInelastically();
        }
    }

    private void initPhysics()
    {
        greenRB = greenKoopa.GetComponent<Rigidbody2D>();
        redRB = redKoopa.GetComponent<Rigidbody2D>();

        // Sets position with relation to velocity
        if(_greenVelocity > 0)
        {
            // If both koopas have a positive velocity
            if(_redVelocity > 0)
            {
                // If the green koopa has a greater velocity, set it behind
                // Else for the red equivalent
                if(_greenVelocity > _redVelocity)
                {
                    greenKoopa.transform.position = new Vector2(-2f, -0.75f);
                    redKoopa.transform.position = new Vector2(-1.4f, -0.75f);
                }
                else
                {
                    redKoopa.transform.position = new Vector2(-2f, -0.75f);
                    greenKoopa.transform.position = new Vector2(-1.4f, -0.75f);
                }
            }
            else
            {
                greenKoopa.transform.position = new Vector2(-2f, -0.75f);
                redKoopa.transform.position = new Vector2(2f, -0.75f);
            }
        }
        else
        {
            // If both have a negative velocity
            if(_redVelocity < 0)
            {
                // If the green koopa has a greater velocity, set it behind
                // Else for the red equivalent
                if(_greenVelocity < _redVelocity)
                {
                    greenKoopa.transform.position = new Vector2(2f, -0.75f);
                    redKoopa.transform.position = new Vector2(1.4f, -0.75f);
                }
                else
                {
                    redKoopa.transform.position = new Vector2(2f, -0.75f);
                    greenKoopa.transform.position = new Vector2(1.4f, -0.75f);
                }
            }
            else
            {
                greenKoopa.transform.position = new Vector2(2f, -0.75f);
                redKoopa.transform.position = new Vector2(-2f, -0.75f);
            }
        }

        // Gets highest speed
        _speedFactor = (Mathf.Abs(_greenVelocity) > Mathf.Abs(_redVelocity)) ? Mathf.Abs(_greenVelocity) / 2 : Mathf.Abs(_redVelocity) / 2;

        // Sets initial velocities of the koopas
        // Scales them down by a factor of the highest speed
        greenRB.velocity = new Vector2(_greenVelocity / _speedFactor, 0.0f);
        redRB.velocity = new Vector2(_redVelocity / _speedFactor, 0.0f);
    }

    private void collideElastically()
    {
        // Calculates final velocities using conservation of momentum formula
        float initialMomentum = (_greenVelocity * _greenMass) + (_redVelocity * _redMass);

        float redFinalVelocity = (initialMomentum - (_greenMass * (_redVelocity - _greenVelocity))) / (_greenMass + _redMass);
        float greenFinalVelocity = redFinalVelocity - (_greenVelocity - _redVelocity);

        // Sets new velocities of the koopas
        // Scales velocities down by a calculated factor
        greenRB.velocity = new Vector2(greenFinalVelocity / _speedFactor, 0.0f);
        redRB.velocity = new Vector2(redFinalVelocity / _speedFactor, 0.0f);

        // Sends values to the results menu
        resultsMenu.setResults(_greenMass, _greenVelocity, greenFinalVelocity, _redMass, _redVelocity, redFinalVelocity);
    }

    private void collideInelastically()
    {
        // Uses conservation of momentum equations to calcualte final velocity
        float initialMomentum = (_greenVelocity * _greenMass) + (_redVelocity * _redMass);
        float finalVelocity = initialMomentum / (_greenMass + _redMass);

        // Sets new velocities of the koopas
        // Scales velocities down by a calculated factor
        greenRB.velocity = new Vector2(finalVelocity / _speedFactor, 0.0f);
        redRB.velocity = new Vector2(finalVelocity / _speedFactor, 0.0f);

        // Sends values to the results menu
        resultsMenu.setResults(_greenMass, _greenVelocity, finalVelocity, _redMass, _redVelocity, finalVelocity);
    }
}
