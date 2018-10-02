using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedKoopa : MonoBehaviour
{
    // Rigidbody object
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Prevents any movement along the y-axis
        if (rb.velocity.y != 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
        }
    } 
}
