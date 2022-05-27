using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool isSwitch, isTriggerArea;
    public string[] turnOnInteraction, turnOffInteraction;
    public string ItemRequested;
    public GameEvent interact;
    public GameObjectArrayVariable inventory;
    public IntVariable itemInHand;
    public StringVariable interactionCode;

    private bool playerInArea, on;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerInArea = true;
        if (isTriggerArea && collision.CompareTag("Player"))
        {
            Interact();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInArea = false;
    }

    public void Interact()
    {
        if (playerInArea && (ItemRequested == "" || (inventory.Value[itemInHand.Value] != null && ItemRequested == inventory.Value[itemInHand.Value].name)))
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
        }
    }

    public IEnumerator CallTurnOnInteractions()
    {
        for (int i = 0; i < turnOnInteraction.Length; i++)
        {
            interactionCode.Value = turnOnInteraction[i];
            interact.Raise();
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator CallTurnOffInteractions()
    {
        for (int i = 0; i < turnOffInteraction.Length; i++)
        {
            interactionCode.Value = turnOffInteraction[i];
            interact.Raise();
            yield return new WaitForEndOfFrame();
        }
    }
}
