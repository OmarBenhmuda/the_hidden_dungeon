using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject menu;

    public float speed = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    private SpriteRenderer _sprite;


    private char _direction = 'D';


    private bool _isPaused = false;


    public GameObject fireballUp;
    public GameObject fireballRight;
    public GameObject fireballDown;
    public GameObject fireballLeft;

    private string abilityQueue = "";


    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        if (_sprite == null)
        {
            Debug.LogError("Player Sprite is missing a renderer");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { HandlePause(); }

        Ability();

        SetDirection();
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.x > 0) { _sprite.flipX = false; }
        else if (movement.x < 0) { _sprite.flipX = true; }

    }



    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void Ability()
    {
        
        if (Input.GetKeyDown(KeyCode.I))
        {
            abilityQueue += 'I';
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            abilityQueue += 'O';
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            abilityQueue += 'P';
        }

        if (abilityQueue == "IOP")
        {
            ShootFireball();
        }

        if (abilityQueue.Length == 3)
        {
            abilityQueue = "";
        }
    }

    private void SetDirection()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) { _direction = 'U'; }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) { _direction = 'R'; }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) { _direction = 'D'; }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) { _direction = 'L'; }
    }

    //This method handles the pausing mechanic of the game
    private void HandlePause()
    {
        if (_isPaused)
        {
            Time.timeScale = 1;
            menu.SetActive(false);
            _isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            menu.SetActive(true);
            _isPaused = true;
        }
    }

    private void ShootFireball()
    {
        if (_direction == 'U')
        {
            var spawnlocation = new Vector2(rb.position.x, rb.position.y + 2f);
            Instantiate(fireballUp, spawnlocation, Quaternion.identity);
        }
        else if (_direction == 'R')
        {
            var spawnlocation = new Vector2(rb.position.x + 2f, rb.position.y);
            Instantiate(fireballRight, spawnlocation, Quaternion.identity);
        }
        else if (_direction == 'D')
        {
            var spawnlocation = new Vector2(rb.position.x, rb.position.y - 2f);
            Instantiate(fireballDown, spawnlocation, Quaternion.identity);
        }
        else if (_direction == 'L')
        {
            var spawnlocation = new Vector2(rb.position.x - 2f, rb.position.y);
            Instantiate(fireballLeft, spawnlocation, Quaternion.identity);
        }

    }
}
