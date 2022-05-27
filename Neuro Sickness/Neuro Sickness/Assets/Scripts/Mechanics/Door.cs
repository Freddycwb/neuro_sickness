using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string openDoorCode, closeDoorCode, stopTimerCode;
    public bool isAreadyOpen, closeAfterPass;
    public float timeToOpen, timeToClose;
    public StringVariable interactionCode;

    private float currentTimeToOpen, currentTimeToClose;

    private void Start()
    {
        if (isAreadyOpen)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    public void Interacted()
    {
        if (interactionCode.Value == openDoorCode)
        {
            Open();
        }
        else if (interactionCode.Value == closeDoorCode)
        {
            Close();
        } 
        else if (interactionCode.Value == stopTimerCode)
        {
            StopTimers();
        }
    }

    private void Update()
    {
        DoorTimer();
    }

    public void DoorTimer()
    {
        if (currentTimeToOpen > 0 && timeToOpen > 0)
        {
            currentTimeToOpen -= Time.deltaTime;
            if (currentTimeToOpen <= 0)
            {
                Open();
                currentTimeToOpen = 0;
            }
        }
        if (currentTimeToClose > 0 && timeToClose > 0)
        {
            currentTimeToClose -= Time.deltaTime;
            if (currentTimeToClose <= 0)
            {
                Close();
                currentTimeToClose = 0;
            }
        }
    }

    public void Open()
    {
        transform.GetComponent<SpriteRenderer>().enabled = false;
        transform.GetComponent<BoxCollider2D>().isTrigger = true;
        if (timeToClose > 0)
        {
            currentTimeToClose = timeToClose;
        }
    }

    public void Close()
    {
        transform.GetComponent<SpriteRenderer>().enabled = true;
        transform.GetComponent<BoxCollider2D>().isTrigger = false;
        if (timeToOpen > 0)
        {
            currentTimeToOpen = timeToOpen;
        }
    }

    public void StopTimers()
    {
        timeToOpen = 0;
        timeToClose = 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (closeAfterPass)
        {
            Close();
        }
    }
}
