using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Tooltip("The max number of seconds it takes for the camera to reach the player.")]
    public float followTime = 1f;

    [Tooltip("The z-distance away from the player the camera should stay at.")]
    public float zOffset = 1f;

    private Vector2 currentVelocity;
    private Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        Vector3 newPosition = Vector2.SmoothDamp(transform.position, player.transform.position, ref currentVelocity, followTime);
        Vector3 positionOffset = new Vector3(0f, 0f, zOffset);
        transform.position = newPosition + positionOffset;
    }
}
