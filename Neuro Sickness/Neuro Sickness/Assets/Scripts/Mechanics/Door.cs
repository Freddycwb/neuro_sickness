using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isAreadyOpen, closeAfterPass;
    public string[] openDoorCode, closeDoorCode, stopTimerCode;
    public float timeToOpen, timeToClose;
    public StringVariable interactionCode;

    private float _currentTimeToOpen, _currentTimeToClose;

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
        for (int i = 0; i < openDoorCode.Length; i++)
        {
            if (interactionCode.Value == openDoorCode[i])
            {
                Open();
            }
        }
        for (int i = 0; i < closeDoorCode.Length; i++)
        {
            if (interactionCode.Value == closeDoorCode[i])
            {
                Close();
            }
        }
        for (int i = 0; i < stopTimerCode.Length; i++)
        {
            if (interactionCode.Value == stopTimerCode[i])
            {
                StopTimers();
            }
        }
    }

    private void Update()
    {
        DoorTimer();
    }

    public void DoorTimer()
    {
        if (_currentTimeToOpen > 0 && timeToOpen > 0)
        {
            _currentTimeToOpen -= Time.deltaTime;
            if (_currentTimeToOpen <= 0)
            {
                Open();
                _currentTimeToOpen = 0;
            }
        }
        if (_currentTimeToClose > 0 && timeToClose > 0)
        {
            _currentTimeToClose -= Time.deltaTime;
            if (_currentTimeToClose <= 0)
            {
                Close();
                _currentTimeToClose = 0;
            }
        }
    }

    public void Open()
    {
        transform.GetComponent<SpriteRenderer>().enabled = false;
        transform.GetComponent<BoxCollider2D>().isTrigger = true;
        if (timeToClose > 0)
        {
            _currentTimeToClose = timeToClose;
        }
    }

    public void Close()
    {
        transform.GetComponent<SpriteRenderer>().enabled = true;
        transform.GetComponent<BoxCollider2D>().isTrigger = false;
        if (timeToOpen > 0)
        {
            _currentTimeToOpen = timeToOpen;
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
