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
    public GameObject powerupIndicator;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent & lt; Rigidbody & gt; ();
        focalPoint = GameObject.Find(&quot; Focal Point&quot;);
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis(&quot; Vertical & quot;);

        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(&quot; Powerup & quot;))
{
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)

    {
        if (collision.gameObject.CompareTag(&quot; Enemy & quot;) &amp; &amp; hasPowerup)
{
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent & lt; Rigidbody & gt; ();
            Vector3 awayFromPLayer = collision.gameObject.transform.position - transform.position;

            enemyRigidbody.AddForce(awayFromPLayer * powerUpStrength, ForceMode.Impulse);
            Debug.Log(&quot; Collided with: &quot; +collision.gameObject.name + &quot; with powerup set to &quot; +
            hasPowerup);
        }
    }
}