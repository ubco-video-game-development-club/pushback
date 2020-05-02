using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed = 1f;

    void Start()
    {
        
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
        Vector3 direction = new Vector3(dx, dy).normalized;
        transform.position += direction * movementSpeed * Time.deltaTime;
    }
}
