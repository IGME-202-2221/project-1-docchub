using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;
    public int health = 25;

    [SerializeField]
    GameObject bullet;
    List<GameObject> bullets = new List<GameObject>();

    Vector3 enemyPosition;
    Vector3 direction = Vector3.zero;
    Vector3 velocity = Vector3.zero;

    const float screenWidthWall = 4f;
    const float screenHeightWall = 5f;

    // Start is called before the first frame update
    void Start()
    {
        enemyPosition = transform.position;
        direction = new Vector3(0, -1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Update veloctity
        velocity = direction * speed * Time.deltaTime;

        // Add velocity to our current position
        enemyPosition += velocity;

        transform.position = enemyPosition;

        // Random chance to fire a bullet
        if (Random.Range(0,1000) == 1)
        {
            Debug.Log("fired an enemy bullet");
            bullets.Add(Instantiate(bullet, transform.position, new Quaternion(0, 0, 180, 0), transform));
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
}
