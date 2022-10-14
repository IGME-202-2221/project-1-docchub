using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberBullet : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;

    [SerializeField]
    Vector3 direction;

    Vector3 bulletPosition;
    Vector3 velocity = Vector3.zero;

    const float screenWidthWall = 4f;
    const float screenHeightWall = 5f;

    // Start is called before the first frame update
    void Start()
    {
        bulletPosition = transform.position;
        direction = direction.normalized;
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
