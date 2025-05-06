using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpforce;
    [SerializeField] private int maxJumps = 2;
    int JumpsRemaining;
    private float moveInput;
    private Rigidbody2D rb2D;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private Vector2 groundCheckDirection = Vector2.down;

    [SerializeField] private int life;
    [SerializeField] private int maxLife;
    [SerializeField] LifeBar barraVida;
    [SerializeField] private Manager gameManager;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        life = maxLife;
        barraVida.InicializeBar(life);
    }

    void FixedUpdate()
    {
        rb2D.linearVelocity = new Vector2(moveInput * speed, rb2D.linearVelocity.y);
        IsGrounded();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemi"))
        {
            SpriteRenderer playerSpriteRenderer = GetComponent<SpriteRenderer>();
            SpriteRenderer enemySpriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();

            if (playerSpriteRenderer.color != enemySpriteRenderer.color)
            {
                TakeDamage(2);
            }
            else
            {
                Debug.Log("Colores iguales, no se recibe da�o.");
            }
        }

        if (collision.gameObject.CompareTag("Insta"))
        {
            gameManager.EndGame(true);

        }else if (collision.gameObject.CompareTag("Respawn"))
        {
            gameManager.EndGame(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Money"))
        {
            int points = 10;
            GameEvents.ScoreUpdated(points);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Heart"))
        {
            int healAmount = 20;
            Heal(healAmount);
            Destroy(other.gameObject);
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>().x;
        Debug.Log("me muevo");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (JumpsRemaining > 0)
        {
            if (context.performed)
            {
                rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpforce);
                JumpsRemaining--;
            }
            else if (context.canceled)
            {
                rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, rb2D.linearVelocity.y * 0.5f);
                JumpsRemaining--;
            }
            Debug.Log("SALTA");
        }
    }

    private void IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, groundCheckDirection, groundCheckDistance, whatIsGround);
        Debug.DrawLine(transform.position, transform.position + Vector3.down, Color.black);

        if (hit.collider != null)
        {
            JumpsRemaining = maxJumps;
        }
        else
        {
            JumpsRemaining = 0;
        }
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
        life = Mathf.Clamp(life, 0, maxLife);

        barraVida.ChangeActualLife(life);
        GameEvents.LifeUpdated(life);

        Debug.Log("Vida");
        if (life == 0)
        {
            GameEvents.GameEnd(false);
            Destroy(gameObject);
        }
    }
    public void Heal(int amount)
    {
        life += amount;
        life = Mathf.Clamp(life, 0, maxLife);

        barraVida.ChangeActualLife(life);
        GameEvents.LifeUpdated(life);
    }
}