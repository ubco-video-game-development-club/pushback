using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed = 1f;

    [Header("Shockwave Attack")]
    public Shockwave shockwavePrefab;
    [Tooltip("Damage dealt per unit of distance the enemy is pushed.")]
    public int shockwaveDamage = 1;
    public float shockwaveSpeed = 1f;
    public float shockwaveMinSize = 1f;
    public float shockwaveMaxSize = 1f;
    public float shockwaveDistance = 1f;
    public float shockwaveDelay = 0.5f;
    public float shockwaveCooldown = 1f;

    private bool alive;
    private float shockwaveTimer;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        alive = true;
        shockwaveTimer = shockwaveCooldown;
    }

    void Update()
    {
        if (alive)
        {
            Move();
            Attack();
        }
    }

    public void Die()
    {
        alive = false;
        animator.SetTrigger("Die");
        LevelController.instance.Lose();
    }

    ///<summary>Move the player based on the user keyboard input.</summary>
    private void Move()
    {
        // Determine the movement direction from keyboard input
        float dx = 0, dy = 0;
        if (Input.GetKey(KeyCode.A))
        {
            dx--;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dx++;
        }
        if (Input.GetKey(KeyCode.W))
        {
            dy++;
        }
        if (Input.GetKey(KeyCode.S))
        {
            dy--;
        }

        bool isMoving = dx != 0 || dy != 0;
        animator.SetBool("IsMoving", isMoving);
        if (dx != 0)
        {
            spriteRenderer.flipX = dx > 0;
        }

        Vector3 direction = new Vector3(dx, dy).normalized;
        transform.position += direction * movementSpeed * Time.deltaTime;
    }

    private void Attack()
    {
        shockwaveTimer += Time.deltaTime;

        if (Input.GetButton("Fire1"))
        {
            if (shockwaveTimer > shockwaveCooldown)
            {
                animator.SetTrigger("Attack");
                StartCoroutine(SpawnShockwave());
                shockwaveTimer = 0;
            }
        }
    }

    private IEnumerator SpawnShockwave()
    {
        yield return new WaitForSeconds(shockwaveDelay);

        // Get direction to the mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dirToMouse = mousePos - transform.position;

        // Instantiate the shockwave object
        Quaternion rotation = Quaternion.FromToRotation(Vector2.right, dirToMouse);
        Shockwave shockwave = Instantiate(shockwavePrefab, transform.position, rotation);

        // Active the shockwave effect
        shockwave.ActivateShockwave(
            transform.position,
            dirToMouse,
            shockwaveDamage,
            shockwaveSpeed,
            shockwaveMinSize,
            shockwaveMaxSize,
            shockwaveDistance
        );
    }
}
