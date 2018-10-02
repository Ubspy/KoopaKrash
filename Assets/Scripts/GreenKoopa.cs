using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenKoopa : MonoBehaviour
{
    // Rigidbody object
    private Rigidbody2D rb;

    // Private collision type bool
    private bool _isElastic;

    // Audio source object
    AudioSource kickSound;

    // Koopa controller object
    public KoopaControl koopaControl;

    // Ending menu object
    public ResultsMenu resultsMenu;

    public void setCollisionType(bool isElastic)
    {
        _isElastic = isElastic;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        kickSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Prevents any movement along the y-axis
        if(rb.velocity.y != 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Checks if it collides with another koopa
        if(collision.gameObject.tag == "Player")
        {
            // Tells the controller to collide the players
            koopaControl.collide();
            StartCoroutine("EndSimulation");

            // If it's inelastic, set tranforms to be parents
            if(!_isElastic)
            {
                // Sets transform parent of red koopa to the green koopas
                collision.gameObject.transform.parent = gameObject.transform;
            }

            // Plays audio clip
            kickSound.Play();
        }
    }

    IEnumerator EndSimulation()
    {
        yield return new WaitForSeconds(2);
        resultsMenu.gameObject.SetActive(true);
    }
}
