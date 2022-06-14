using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Vector3 initialPos;

    void Start()
    {
        initialPos = transform.position;
    }

    public void Respawn()
    {
        transform.position = initialPos;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Damage" && !collision.isTrigger) {
            Respawn();
        }
    }
}
