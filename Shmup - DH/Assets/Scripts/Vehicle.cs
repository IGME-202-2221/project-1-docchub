 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Vehicle : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;

    Vector3 vehiclePosition = Vector3.zero;
    Vector3 direction = Vector3.zero;
    Vector3 velocity = Vector3.zero;

    const float screenWidthWall = 3;
    const float screenHeightWall = 5;

    // Start is called before the first frame update
    void Start()
    {
        vehiclePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {        
        // Update veloctity
        velocity = direction * speed * Time.deltaTime;

        // Add velocity to our current position
        vehiclePosition += velocity;

        transform.position = vehiclePosition;

        // Prevent moving outside the bounds
        if (vehiclePosition.x >= screenWidthWall)
        {
            vehiclePosition = new Vector3(screenWidthWall, vehiclePosition.y, 0);
            transform.position = new Vector3(screenWidthWall, vehiclePosition.y, 0);
        }
        else if (vehiclePosition.x <= -screenWidthWall)
        {
            vehiclePosition = new Vector3(-screenWidthWall, vehiclePosition.y, 0);
            transform.position = new Vector3(-screenWidthWall, vehiclePosition.y, 0);
        }
        if (vehiclePosition.y >= screenHeightWall)
        {
            vehiclePosition = new Vector3(vehiclePosition.x, screenHeightWall, 0);
            transform.position = new Vector3(vehiclePosition.x, screenHeightWall, 0);
        }
        else if (vehiclePosition.y <= -screenHeightWall)
        {
            vehiclePosition = new Vector3(vehiclePosition.x, -screenHeightWall, 0);
            transform.position = new Vector3(vehiclePosition.x, -screenHeightWall, 0);
        }
    }

    /// <summary>
    /// Checks for player inputs
    /// </summary>
    /// <param name="context"></param>
    public void OnMove(InputAction.CallbackContext context)
    {    
        direction = context.ReadValue<Vector2>();
    }
}
