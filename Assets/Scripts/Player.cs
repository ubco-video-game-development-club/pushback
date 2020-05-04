using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed = 1f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
    }

    ///<summary>Move the player based on the user keyboard input.</summary>
    private void Move()
    {
        // Determine the player direction from keyboard input
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
}
