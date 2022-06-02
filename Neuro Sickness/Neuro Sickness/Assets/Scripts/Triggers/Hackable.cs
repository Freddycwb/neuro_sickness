using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hackable : MonoBehaviour
{
    public GameEvent hack;
    public bool isSwitch;
    public string[] turnOnHack, turnOffHack;
    public minigamesType minigame;
    public int difficulty;
    public StringVariable minigameRequested;
    public FloatVariable minigameDifficulty;
    public StringVariable interactionCode;
    public GameEvent interact;
    public BoolVariable canControl;

    public enum minigamesType
    {
        Spam,
        Osu
    }

    private bool beingHacked, on;

    private void OnMouseDown()
    {
        if (canControl.Value)
        {
            minigameRequested.Value = minigame.ToString();
            minigameDifficulty.Value = difficulty;
            beingHacked = true;
            hack.Raise();
        }
    }

    public void Interact()
    {
        if (beingHacked)
        {
            if (isSwitch)
            {
                if (!on)
                {
                    StartCoroutine(CallTurnOnInteractions());
                    on = true;
                }
                else
                {
                    StartCoroutine(CallTurnOffInteractions());
                    on = false;
                }
            }
            else
            {
                StartCoroutine(CallTurnOnInteractions());
            }
            interact.Raise();
            beingHacked = false;
        }
    }

    public IEnumerator CallTurnOnInteractions()
    {
        for (int i = 0; i < turnOnHack.Length; i++)
        {
            interactionCode.Value = turnOnHack[i];
            interact.Raise();
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator CallTurnOffInteractions()
    {
        for (int i = 0; i < turnOffHack.Length; i++)
        {
            interactionCode.Value = turnOffHack[i];
            interact.Raise();
            yield return new WaitForEndOfFrame();
        }
    }

    public void CancelHack()
    {
        beingHacked = false;
    }
}
