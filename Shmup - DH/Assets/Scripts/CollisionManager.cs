using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [SerializeField]
    GameObject playerPrefab;

    GameObject player;
    List<GameObject> playerBullets = new List<GameObject>();
    List<GameObject> enemies = new List<GameObject>();
    List<GameObject> enemyBullets = new List<GameObject>();

    private void Start()
    {
        player = Instantiate(playerPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerBulletsOnEnemies();
        CheckEnemyBulletsOnPlayer();
    }

    /// <summary>
    /// Collision between enemies and the player's bullets
    /// </summary>
    void CheckPlayerBulletsOnEnemies()
    {
        // Update Lists
        if (FindObjectOfType<Spawner>().GetEnemies() != null)
        {
            enemies = FindObjectOfType<Spawner>().GetEnemies();
        }
        if (FindObjectOfType<Vehicle>().GetPlayerBullets() != null)
        {
            playerBullets = FindObjectOfType<Vehicle>().GetPlayerBullets();
        }

        // Check for collisions between enemies and the player's bullets
        if (playerBullets.Count > 0 && enemies.Count > 0)
        {
            foreach (GameObject a in enemies)
            {
                foreach (GameObject b in playerBullets)
                {
                    if (GetComponent<CollisionDetection>().AABBCollision(a, b))
                    {
                        Debug.Log("Saw a collision");

                        // Deal damage to enemy
                        a.GetComponent<Enemy>().health -= 5;

                        // Destroy the bullet that collided with the enemy
                        Destroy(b);
                        playerBullets.Remove(b);

                        return;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Collision between player and enemy bullets
    /// </summary>
    void CheckEnemyBulletsOnPlayer()
    {
        // Update Lists
        if (FindObjectOfType<Spawner>().GetEnemies() != null)
        {
            enemies = FindObjectOfType<Spawner>().GetEnemies();
        }
        foreach (GameObject enemy in enemies)
        {
            enemyBullets = enemy.GetComponent<Enemy>().GetEnemyBullets();

            // Check for collisions between the player and the enemy's bullets
            if (enemyBullets != null && enemyBullets.Count > 0)
            {
                foreach (GameObject b in enemyBullets)
                {
                    if (GetComponent<CollisionDetection>().AABBCollision(player, b))
                    {
                        Debug.Log("Saw a collision");

                        // Deal damage to player
                        player.GetComponent<Vehicle>().health -= 5;

                        // Destroy the bullet that collided with the enemy
                        Destroy(b);
                        enemyBullets.Remove(b);

                        return;
                    }
                }
            }
        }
    }
}
