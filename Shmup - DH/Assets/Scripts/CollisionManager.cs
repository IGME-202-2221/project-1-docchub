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
                        // Deal damage to enemy
                        if (a.name == "enemy1(Clone)")
                        {
                            a.GetComponent<Enemy>().health -= 5;
                        }
                        else if (a.name == "asteroid(Clone)" || a.name == "asteroidChild(Clone)")
                        {
                            a.GetComponent<Asteroid>().health -= 5;
                        }

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
            // Only check for bullets from certain enemies
            if (enemy.name == "enemy1(Clone)")
            {
                enemyBullets = enemy.GetComponent<Enemy>().GetEnemyBullets();

                // Check for collisions between the player and the enemy's bullets
                if (enemyBullets != null && enemyBullets.Count > 0)
                {
                    foreach (GameObject b in enemyBullets)
                    {
                        if (GetComponent<CollisionDetection>().AABBCollision(player, b))
                        {
                            // Deal damage to player
                            player.GetComponent<Vehicle>().health -= 5;
                            FindObjectOfType<Timer>().health -= 5;
                            FindObjectOfType<HealthBar>().SetHealth(player.GetComponent<Vehicle>().health);

                            // Destroy the bullet that collided with the enemy
                            Destroy(b);
                            enemyBullets.Remove(b);

                            return;
                        }
                    }
                }
            }
            
            // Collisions for Asteroids on Player
            else if (enemy.name == "asteroid(Clone)" || enemy.name == "asteroidChild(Clone)")
            {
                if (GetComponent<CollisionDetection>().AABBCollision(player, enemy))
                {
                    // Deal damage to player
                    player.GetComponent<Vehicle>().health -= 5;
                    FindObjectOfType<Timer>().health -= 5;
                    FindObjectOfType<HealthBar>().SetHealth(player.GetComponent<Vehicle>().health);

                    // Destroy the object that collided with the enemy
                    Destroy(enemy);
                    enemies.Remove(enemy);

                    return;
                }
            }
        }
    }
}
