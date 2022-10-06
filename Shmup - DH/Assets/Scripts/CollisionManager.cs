using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    public List<GameObject> playerBullets = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> enemyBullets = new List<GameObject>();

    // Update is called once per frame
    void Update()
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
                foreach(GameObject b in playerBullets)
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
}
