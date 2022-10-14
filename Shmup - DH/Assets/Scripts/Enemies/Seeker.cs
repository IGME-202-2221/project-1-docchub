using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;
    public int health = 25;
    int prevHealth;

    [SerializeField]
    GameObject bullet;
    List<GameObject> bullets = new List<GameObject>();

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
        direction = new Vector3(1, 0, 0);

        prevHealth = health;

        // Random chance to fire a bullet
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        // Update veloctity
        velocity = direction * speed * Time.deltaTime;

        // Add velocity to our current position
        enemyPosition += velocity;

        transform.position = enemyPosition;

        // Indicate if health is lost
        if (prevHealth > health)
        {
            StartCoroutine(CheckHealth());
            prevHealth = health;
        }

        // Clean up stray bullets
        foreach (GameObject b in bullets)
        {
            if (b.transform.position.y < -screenHeightWall ||
                b.transform.position.x > screenWidthWall ||
                b.transform.position.x < -screenWidthWall)
            {
                Destroy(b);
                bullets.Remove(b);
                return;
            }
        }

        // Bounce back and forth horizontally
        if (transform.position.x >= screenWidthWall)
        {
            direction = new Vector3(-1, 0, 0);
        }
        else if (transform.position.x <= -screenWidthWall)
        {
            direction = new Vector3(1, 0, 0);
        }
    }

    IEnumerator Shoot()
    {
        while (health > 500)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            bullets.Add(Instantiate(bullet, transform.position, Quaternion.identity, transform));
        }
        
        if (health <= 500)
        {
            speed = speed * 3;
        }

        while (health <= 500)
        {
            yield return new WaitForSecondsRealtime(0.2f);
            bullets.Add(Instantiate(bullet, transform.position, Quaternion.identity, transform));
        }
    }

    public List<GameObject> GetEnemyBullets()
    {
        if (bullets != null && bullets.Count > 0)
        {
            return bullets;
        }
        else
        {
            return null;
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
