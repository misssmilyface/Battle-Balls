using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private float powerUpStrength = 15.0f;
    public float speed = 5.0f;
    public bool hasPowerup = false;
    public GameObject powerupIndicator; // getting the gameObject of Powerup Indicator.

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput); // makes the camera move with it. 4-1 step6
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0); // we are setting the powerIndicator onto the player so it'll follow the player around when "hasPowerup = true". 
                                                                                             //We added a new Vector so the "y" position would be lower, meaning that it would be close to the ground.
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());// Start Co-routines(that's how it's pronounced): is able to run a countdown timer with a function called WaitForSeconds().
            powerupIndicator.gameObject.SetActive(true); // when "hasPowerup = true", the powerupIndicator will be turned on.
        }
    }

    IEnumerator PowerupCountdownRoutine() // I-Enumerator(That's how it's pernounced):In C#, it's called an interface. It basically in this situation lets you add a timer (enable a countdown timer) outside of our Update() loop.
    {
        yield return new WaitForSeconds(7);// yield: This will enable us to run this timer in a place outside of our Update() loop.
        hasPowerup = false; // after the timer is up, we set the powerup back to false.
        powerupIndicator.gameObject.SetActive(false); // when "hasPowerup = false", the powerupIndicator will be turned off.
    }

    void OnCollisionEnter(Collision collision) // when you want to try and use physics, you use this.
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>(); // getting the Rigidbody from the enemy.
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position); // trying to find the distance between the enemy and the player.
            Debug.Log("Collided with" + collision.gameObject.name + " with powerup set to " + hasPowerup); // writes this message when you collide with an enemy.
            // The line above is called "Concatenation". It can add strings together to create one entire full message.
            enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse); // adding a force so the enemies would bounce off the player when the player has the power up.
        } 
    }
}
