using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorBullet : MonoBehaviour
{
    [SerializeField]
    float speed = 3f;

    Vector3 bulletPosition;
    Vector3 direction = Vector3.zero;
    Vector3 velocity = Vector3.zero;

    const float screenWidthWall = 4f;
    const float screenHeightWall = 5f;

    // Start is called before the first frame update
    void Start()
    {
        bulletPosition = transform.position;

        Vector3 playerPos = FindObjectOfType<Vehicle>().transform.position;
        float x = playerPos.x - bulletPosition.x;
        float y = playerPos.y - bulletPosition.y;
        direction = new Vector3(x, y, 0).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        // Update veloctity
        velocity = direction * speed * Time.deltaTime;

        // Add velocity to our current position
        bulletPosition += velocity;

        transform.position = bulletPosition;
    }
}
