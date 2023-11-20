using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    private Rigidbody enemyRb;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        // adding force to the enemyRb so that it can follow the player around...
        // it'll start by finding the distance between the player and the enemy then adding the speed to it.
        // normalized: because when the enemy is really far away from the player the speed would naturally be different so if we add a normalize in this case, it wouldn't be coming at the player so quick anymore. It would be back to normal speed.
        enemyRb.AddForce( lookDirection * speed);
        if (transform.position.y < -10)
        {
            Destroy(gameObject); // destroy the enemy if it's y position is lower than -10.
        }
    }
}
