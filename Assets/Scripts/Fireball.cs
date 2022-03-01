using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float vel = 15f;
    public char direction;
    private Rigidbody2D _rb;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3.0f);
    }

    // Update is called once per frame
    private void Update()
    {
        if (direction == 'U')
        {
            _rb.velocity = new Vector2(0, vel); 
        }
        else if (direction == 'R')
        {
            _rb.velocity = new Vector2(vel, 0);
        }
        else if (direction == 'D')
        {
            _rb.velocity = new Vector2(0, -vel);
        }
        else if (direction == 'L')
        {
            _rb.velocity = new Vector2(-vel, 0);
        }

    }

    private void OnCollisionStay2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
