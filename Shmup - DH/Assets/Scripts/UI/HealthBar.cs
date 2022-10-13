using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider healthBar;

    public void SetHealth(int health)
    {
        healthBar.value = Mathf.Abs(health - 100);
    }
}
