using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public IntVariable health;
    public Image[] sprites;

    public GameEvent death;

    public void AddHealth()
    {
        health.Value += 1;
        UpdateUI();
    }

    public void SubtractHealth()
    {
        health.Value -= 1;
        if (health.Value <= 0)
        {
            death.Raise();
        }
        UpdateUI();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].enabled = false;
        }
        for (int i = 0; i < health.Value; i++)
        {
            sprites[i].enabled = true;
        }
    }
}
