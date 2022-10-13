using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;

    [SerializeField]
    GameObject asteroid;

    [SerializeField]
    GameObject children;

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
    }

    // Update is called once per frame
    void Update()
    {
        // Delete enemies after they exit the bounds of the screen
        foreach (GameObject e in enemies)
        {
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
                    FindObjectOfType<Score>().ScoreUpdate();
                    Destroy(e);
                    enemies.Remove(e);
                    return;
                }
            }            
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
                    FindObjectOfType<Score>().ScoreUpdate();
                    OnDeath(e);
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
