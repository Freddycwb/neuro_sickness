using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoreDialogs : MonoBehaviour
{
    public string dialog;
    private bool playerInArea;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInArea = false;
    }

    public void Interacted()
    {
        if (playerInArea)
        {
            GetComponent<AIDialogs>().Speech(dialog);
        }
    }
}
