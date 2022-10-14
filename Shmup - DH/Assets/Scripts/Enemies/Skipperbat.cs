using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skipperbat : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;
    public int health = 40;
    int prevHealth;

    [SerializeField]
    GameObject bullet;
    List<GameObject> bullets = new List<GameObject>();

    Vector3 enemyPosition;
    Vector3 direction = Vector3.zero;
    Vector3 velocity = Vector3.zero;

    float rotateDeg;

    const float screenWidthWall = 4f;
    const float screenHeightWall = 5f;

    const float hitStateLength = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        enemyPosition = transform.position;
        prevHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        // Track player
        Transform playerPos = FindObjectOfType<Vehicle>().transform;
        float x = playerPos.position.x - enemyPosition.x;
        float y = playerPos.position.y - enemyPosition.y;
        direction = new Vector3(x, y, 0).normalized;

        // Update veloctity
        velocity = direction * speed * Time.deltaTime;

        // Add velocity to our current position
        enemyPosition += velocity;

        // Draw calculated position
        transform.position = enemyPosition;

        // Calculate Rotation
        transform.rotation = Quaternion.LookRotation(Vector3.back, direction);

        // Indicate if health is lost
        if (prevHealth > health)
        {
            StartCoroutine(CheckHealth());
            prevHealth = health;
        }

        // Clean up stray bullets
        foreach (GameObject b in bullets)
        {
            if (b.transform.position.y < -screenHeightWall)
            {
                Destroy(b);
                bullets.Remove(b);
                return;
            }
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