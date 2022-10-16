using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;

    [SerializeField]
    GameObject asteroid;

    [SerializeField]
    GameObject children;

    [SerializeField]
    GameObject seeker;

    [SerializeField]
    GameObject skipperbat;

    [SerializeField]
    GameObject bomber;

    [SerializeField]
    Vector2[] spawnPositions;

    List<GameObject> enemies = new List<GameObject>();

    const float screenWidthWall = 4f;
    const float screenHeightWall = 6f;

    const float enemySpawnRate = 10f;

    bool spawnEnemies;

    // Start is called before the first frame update
    void Start()
    {
        spawnEnemies = true;
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnAsteroids());
        StartCoroutine(SpawnBats());
        StartCoroutine(SpawnBombers());

        SpawnSeeker();
    }

    // Update is called once per frame
    void Update()
    {
        // Delete enemies after they exit the bounds of the screen
        foreach (GameObject e in enemies)
        {
            // Delete logic for vanilla enemy
            if (e.name == "enemy1(Clone)")
            {
                if (e.transform.position.y < -screenHeightWall)
                {
                    Destroy(e);
                    enemies.Remove(e);
                    return;
                }

                // Only increase score if the player killed the enemy
                else if (e.GetComponent<Enemy>().health <= 0)
                {
                    FindObjectOfType<Score>().ScoreUpdate(10);
                    Destroy(e);
                    enemies.Remove(e);
                    return;
                }
            }

            // Delete logic for asteroid enemy
            else if (e.name == "asteroid(Clone)" || e.name == "asteroidChild(Clone)")
            {
                if (e.transform.position.y < -screenHeightWall)
                {
                    Destroy(e);
                    enemies.Remove(e);
                    return;
                }

                // Only increase score if the player killed the enemy
                else if (e.GetComponent<Asteroid>().health <= 0)
                {
                    FindObjectOfType<Score>().ScoreUpdate(20);
                    OnDeath(e);
                    Destroy(e);
                    enemies.Remove(e);
                    return;
                }
            }

            // Delete logic for seeker enemy
            else if (e.name == "seeker(Clone)")
            {
                // Increase score if the player killed the enemy
                if (e.GetComponent<Seeker>().health <= 0)
                {
                    FindObjectOfType<Score>().ScoreUpdate(500);
                    Destroy(e);
                    enemies.Remove(e);
                    return;
                }
            }

            else if (e.name == "skipperbat(Clone)")
            {
                // Increase score if the player killed the enemy
                if (e.GetComponent<Skipperbat>().health <= 0)
                {
                    FindObjectOfType<Score>().ScoreUpdate(50);
                    Destroy(e);
                    enemies.Remove(e);
                    return;
                }
            }

            else if (e.name == "bomber(Clone)")
            {
                // Increase score if the player killed the enemy
                if (e.GetComponent<Bomber>().health <= 0)
                {
                    FindObjectOfType<Score>().ScoreUpdate(35);
                    Destroy(e);
                    enemies.Remove(e);
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Spawns enemies during the game
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnEnemies()
    {
        // Enemy1 spawn rate
        while (spawnEnemies)
        {
            Spawn();
            yield return new WaitForSeconds(enemySpawnRate);
        }
    }
    IEnumerator SpawnAsteroids()
    {
        while (spawnEnemies)
        {
            SpawnAsteroid();
            yield return new WaitForSeconds(Random.Range(3f, 7f));
        }
    }
    IEnumerator SpawnBats()
    {
        while (spawnEnemies)
        {
            SpawnBat();
            yield return new WaitForSeconds(Random.Range(7f, 12f));
        }
    }

    IEnumerator SpawnBombers()
    {
        while (spawnEnemies)
        {
            SpawnBomber();
            yield return new WaitForSeconds(Random.Range(7f, 12f));
        }
    }

    /// <summary>
    /// Spawns a set of enemy1
    /// </summary>
    void Spawn()
    {
        foreach (Vector2 p in spawnPositions)
        {
            enemies.Add(Instantiate(enemy, (Vector3)p, new Quaternion(0, 0, 180, 0), transform));
        }
    }

    /// <summary>
    /// Spawns an asteroid
    /// </summary>
    void SpawnAsteroid()
    {
        enemies.Add(Instantiate(asteroid, new Vector2(Random.Range(-2f, 2f), 5.5f), Quaternion.identity, transform));
    }

    /// <summary>
    /// Spawns a seeker enemy
    /// </summary>
    void SpawnSeeker()
    {
        enemies.Add(Instantiate(seeker, new Vector2(0, 4), new Quaternion(0, 0, 180, 0), transform));
    }

    void SpawnBat()
    {
        enemies.Add(Instantiate(skipperbat, new Vector2(Random.Range(-screenWidthWall, screenWidthWall), 5.5f), Quaternion.identity, transform));
    }

    void SpawnBomber()
    {
        enemies.Add(Instantiate(bomber, new Vector2(Random.Range(-2f, 2f), 5.5f), new Quaternion(0,0,180,0), transform));
    }

    /// <summary>
    /// Spawns asteroid children
    /// </summary>
    void OnDeath(GameObject asteroid)
    {
        if (asteroid.GetComponent<Asteroid>().hasChildren)
        {
            for (int i = 0; i < 2; i++)
            {
                enemies.Add(Instantiate(
                        children, 
                        new Vector3(asteroid.transform.position.x, asteroid.transform.position.y, 0), 
                        Quaternion.identity));
            }
        }
    }

    /// <summary>
    /// Returns a list of enemies to the collision manager
    /// </summary>
    /// <returns></returns>
    public List<GameObject> GetEnemies()
    {
        if (enemies != null && enemies.Count > 0)
        {
            return enemies;
        }
        else
        {
            return null;
        }
    }
}
