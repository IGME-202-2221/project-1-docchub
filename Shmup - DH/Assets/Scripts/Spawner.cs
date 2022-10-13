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
    Vector2[] spawnPositions;

    [SerializeField]
    Vector2 asteroidSpawnPos;

    List<GameObject> enemies = new List<GameObject>();

    const float screenWidthWall = 4f;
    const float screenHeightWall = 6f;

    const float enemySpawnRate = 10f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());

        SpawnAsteroid();
    }

    // Update is called once per frame
    void Update()
    {
        // Delete enemies after they exit the bounds of the screen
        foreach (GameObject e in enemies)
        {
            if (e.name == "enemy1(Clone)")
            {
                if (e.transform.position.y < -screenHeightWall ||
                    e.GetComponent<Enemy>().health <= 0)
                {
                    Destroy(e);
                    enemies.Remove(e);
                    return;
                }
            }            
            else if (e.name == "asteroid(Clone)" || e.name == "asteroidChild(Clone)")
            {
                if (e.transform.position.y < -screenHeightWall ||
                    e.GetComponent<Asteroid>().health <= 0)
                {
                    FindObjectOfType<Asteroid>().OnDeath();
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
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(enemySpawnRate);
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
        enemies.Add(Instantiate(asteroid, asteroidSpawnPos, Quaternion.identity, transform));
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
