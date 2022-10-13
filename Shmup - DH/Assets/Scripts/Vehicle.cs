 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.LowLevel;

public class Vehicle : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;
    public int health;
    int prevHealth;

    [SerializeField]
    GameObject bullet;
    List<GameObject> bullets = new List<GameObject>();

    Vector3 vehiclePosition = Vector3.zero;
    Vector3 direction = Vector3.zero;
    Vector3 velocity = Vector3.zero;

    [SerializeField]
    float fireRate;
    bool rapidFire;
    bool cRunning;

    const float screenWidthWall = 4f;
    const float screenHeightWall = 5f;

    const float hitStateLength = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        vehiclePosition = transform.position;
        rapidFire = false;

        prevHealth = health;
    }

    // Update is called once per frame
    void Update()
    {        
        // Update veloctity
        velocity = direction * speed * Time.deltaTime;

        // Add velocity to our current position
        vehiclePosition += velocity;

        // Draw at calculated position
        transform.position = vehiclePosition;

        // Visualize losing health
        if (prevHealth > health)
        {
            StartCoroutine(CheckHealth());
            prevHealth = health;
        }

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

        // Clean up stray bullets
        foreach (GameObject b in bullets)
        {
            if (b.transform.position.y > screenHeightWall)
            {
                Destroy(b);
                bullets.Remove(b);
                return;
            }
        }

        // Rapid fire mode
        if (rapidFire && !cRunning)
        {
            StartCoroutine(RapidFire());
        }
        else if (!rapidFire)
        {
            StopCoroutine(RapidFire());
            cRunning = false;
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

    /// <summary>
    /// Spawns a bullet
    /// </summary>
    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("attempted to spawn bullet");
            bullets.Add(Instantiate(bullet, transform.position, Quaternion.identity, transform));
            rapidFire = true;
        }

        if (context.canceled)
        {
            Debug.Log("fire button released");
            rapidFire = false;
        }
    }

    public List<GameObject> GetPlayerBullets()
    {
        if (bullets != null && bullets.Count > 0)
        {
            return bullets;
        }
        else
        {
            return null;
        }
    }

    IEnumerator RapidFire()
    {
        cRunning = true;

        while (rapidFire)
        {
            yield return new WaitForSecondsRealtime(fireRate);
            bullets.Add(Instantiate(bullet, transform.position, Quaternion.identity, transform));
        }
    }

    /// <summary>
    /// Adds a visual component to losing health
    /// </summary>
    IEnumerator CheckHealth()
    {
        this.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSecondsRealtime(hitStateLength);
        this.GetComponent<SpriteRenderer>().color = Color.white;
        StopCoroutine(CheckHealth());
    }
}
