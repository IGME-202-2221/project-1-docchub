using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;

    [SerializeField]
    Vector2[] spawnPositions;

    List<GameObject> enemies = new List<GameObject>();

    const float screenWidthWall = 4f;
    const float screenHeightWall = 6f;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        // Delete enemies after they exit the bounds of the screen
        foreach (GameObject e in enemies)
        {
            if (e.transform.position.y < -screenHeightWall ||
                e.GetComponent<Enemy>().health <= 0)
            {
                Destroy(e);
                enemies.Remove(e);
                return;
            }
        }
    }

    void Spawn()
    {
        foreach (Vector2 p in spawnPositions)
        {
            enemies.Add(Instantiate(enemy, (Vector3)p, new Quaternion(0, 0, 180, 0), transform));
        }
    }

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
