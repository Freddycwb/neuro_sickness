using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public Image bar;
    public BoolVariable isRunning;
    public BoolVariable holdingRun;
    public FloatVariable stamina;
    public IntVariable maxStamina;

    void Update()
    {
        StaminaSystem();
        UpdateUI();
    }

    private void StaminaSystem()
    {
        if (holdingRun.Value && stamina.Value > 0)
        {
            isRunning.Value = true;
        }
        else
        {
            isRunning.Value = false;
        }
        if (holdingRun.Value)
        {
            if (stamina.Value > 0)
            {
                stamina.Value -= Time.deltaTime;
            }
            else
            {
                stamina.Value = 0;
            }
        }
        else
        {
            if (stamina.Value < maxStamina.Value)
            {
                stamina.Value += Time.deltaTime / 1.37f;
            }
            else
            {
                stamina.Value = maxStamina.Value;
            }
        }
    }

    private void UpdateUI()
    {
        bar.fillAmount = stamina.Value / maxStamina.Value;
    }
}
