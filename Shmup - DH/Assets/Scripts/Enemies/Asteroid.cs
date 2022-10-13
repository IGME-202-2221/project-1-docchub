using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;
    public int health = 25;
    int prevHealth;

    public bool hasChildren;

    Vector3 enemyPosition;
    Vector3 direction = Vector3.zero;
    Vector3 velocity = Vector3.zero;

    const float screenWidthWall = 4f;
    const float screenHeightWall = 5f;

    const float hitStateLength = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        enemyPosition = transform.position;
        direction = new Vector3(Random.Range(-0.8f, 0.8f), -1, 0).normalized;

        prevHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        // Update veloctity
        velocity = direction * speed * Time.deltaTime;

        // Add velocity to our current position
        enemyPosition += velocity;

        // Draw at calculated position
        transform.position = enemyPosition;

        // Bounce off walls
        if (transform.position.x <= -screenWidthWall)
        {
            direction = new Vector3(Random.Range(0.1f, 0.7f), -1, 0).normalized;
        }
        else if (transform.position.x >= screenWidthWall)
        {
            direction = new Vector3(Random.Range(-0.1f, -0.7f), -1, 0).normalized;
        }

        // Indicate if health is lost
        if (prevHealth > health)
        {
            StartCoroutine(CheckHealth());
            prevHealth = health;
        }
    }

    /// <summary>
    /// Adds a visual component to losing health
    /// </summary>
    IEnumerator CheckHealth()
    {
        this.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSecondsRealtime(hitStateLength);
        this.GetComponent<SpriteRenderer>().color = Color.white;
        StopCoroutine(CheckHealth());
    }
}
