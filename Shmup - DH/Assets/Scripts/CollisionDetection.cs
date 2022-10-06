using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    SpriteRenderer a;
    SpriteRenderer b;

    float aWidth;
    float aHeight;
    float bWidth;
    float bHeight;

    float aMaxX;
    float aMinX;
    float aMaxY;
    float aMinY;

    float bMaxX;
    float bMinX;
    float bMaxY;
    float bMinY;

    /// <summary>
    /// Collision detection using bounding boxes
    /// </summary>
    /// <param name="objectA"></param>
    /// <param name="objectB"></param>
    /// <returns></returns>
    public bool AABBCollision(GameObject objectA, GameObject objectB)
    {
        // Get both objects' spriterenderers
        a = objectA.GetComponent<SpriteRenderer>();
        b = objectB.GetComponent<SpriteRenderer>();

        // Determine object widths and heights
        aWidth = a.bounds.size.x / 2;
        aHeight = a.bounds.size.y / 2;
        bWidth = b.bounds.size.x / 2;
        bHeight = b.bounds.size.y / 2;

        // Max width and height
        aMaxX = objectA.transform.position.x + aWidth;
        aMinX = objectA.transform.position.x - aWidth;
        aMaxY = objectA.transform.position.y + aHeight;
        aMinY = objectA.transform.position.y - aHeight;
        bMaxX = objectB.transform.position.x + bWidth;
        bMinX = objectB.transform.position.x - bWidth;
        bMaxY = objectB.transform.position.y + bHeight;
        bMinY = objectB.transform.position.y - bHeight;

        // Check to see if the vehicle and a given object are colliding
        if (aMaxX > bMinX && aMinX < bMaxX && aMinY < bMaxY && aMaxY > bMinY)
        {
            Debug.Log("AABB collision");
            return true;
        }
        else
        {
            return false;
        }
    }
}
