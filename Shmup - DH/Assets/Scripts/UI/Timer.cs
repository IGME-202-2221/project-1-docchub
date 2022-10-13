using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI timer;

    public int health = 100;

    int minutes = 2;
    int seconds = 0;

    private void Start()
    {
        StartCoroutine(Clock());
    }

    IEnumerator Clock()
    {
        while ((minutes != 0 || seconds != 0) && health > 0)
        {
            yield return new WaitForSeconds(1f);

            // update time
            if (seconds == 0)
            {
                minutes--;
                seconds = 59;
            }
            else
            {
                seconds--;
            }

            if (seconds >= 0 && seconds < 10)
            {
                timer.text = string.Format("{0}:0{1}", minutes, seconds);
            }
            else
            {
                timer.text = string.Format("{0}:{1}", minutes, seconds);
            }
        }

        if (health > 0)
        {
            timer.color = Color.green;
            timer.text = "YOU WIN";
        }
        else
        {
            timer.color = Color.red;
            timer.text = "GAME OVER";
        }

        StopAllCoroutines();
    }
}
