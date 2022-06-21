using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDialogTrigger : MonoBehaviour
{
    public string dialog;
    public bool destroyAfterUse;

    public GameObjectVariable player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindObjectOfType<AIMoviment>().gameObject.GetComponent<AIDialogs>().Speech(dialog);
            if (destroyAfterUse)
            {
                Destroy(gameObject);
            }
        }
    }
}
