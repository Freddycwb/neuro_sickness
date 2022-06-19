using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isAreadyOpen, closeAfterPass, isUnlocked;
    private bool isOpen;
    public string[] openDoorCode, closeDoorCode, stopTimerCode, toggleDoorStateCode;
    public float timeToOpen, timeToClose, timeToStartCouter;
    public StringVariable interactionCode;
    public SoundVariable openSound, closeSound, laserSound;

    private Animator _animatorFrontDoor, _animatorSideDoor;
    private float _currentTimeToOpen, _currentTimeToClose;
    private AudioSource _audio;
    private bool firstState;

    private void Start()
    {
        if (tag != "Damage")
        {
            transform.GetComponent<SpriteRenderer>().enabled = false;
            _animatorFrontDoor = transform.parent.GetChild(1).GetChild(0).GetComponent<Animator>();
            _animatorSideDoor = transform.parent.GetChild(1).GetChild(1).GetComponent<Animator>();
            _audio = transform.GetChild(0).GetComponent<AudioSource>();
        }
        else
        {
            _audio = GetComponent<AudioSource>();
        }
        if (isAreadyOpen)
        {
            Open();
        }
        else
        {
            Close();
        }
        isOpen = isAreadyOpen;
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
        for (int i = 0; i < toggleDoorStateCode.Length; i++)
        {
            if (interactionCode.Value == toggleDoorStateCode[i])
            {
                Toggle();
            }
        }
    }

    private void Update()
    {
        DoorTimer();
    }

    public void DoorTimer()
    {
        if (timeToStartCouter == 0)
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
        else
        {
            timeToStartCouter -= Time.deltaTime;
            if (timeToStartCouter <= 0)
            {
                timeToStartCouter = 0;
            }
        }
    }

    public void Open()
    {
        if (tag != "Damage")
        {
            _animatorFrontDoor.Play("DoorFrontOpen");
            _animatorSideDoor.Play("DoorSideOpen");
            if (firstState)
            {
                _audio.clip = openSound.Value;
                _audio.Play();
            }
        }
        else
        {
            transform.GetComponent<SpriteRenderer>().enabled = false;
            if (firstState)
            {
                _audio.Stop();
            }
        }
        transform.GetComponent<BoxCollider2D>().isTrigger = true;
        if (timeToClose > 0)
        {
            _currentTimeToClose = timeToClose;
        }
        firstState = true;
        isOpen = true;
    }

    public void Close()
    {
        if (tag != "Damage")
        {
            _animatorFrontDoor.Play("DoorFrontClose");
            _animatorSideDoor.Play("DoorSideClose");
            if (firstState)
            {
                _audio.clip = closeSound.Value;
                _audio.Play();
            }
        }
        else
        {
            transform.GetComponent<SpriteRenderer>().enabled = true;
            if (firstState)
            {
                _audio.clip = laserSound.Value;
                _audio.Play();
            }
        }
        if (!isUnlocked)
        {
            transform.GetComponent<BoxCollider2D>().isTrigger = false;
        }
        if (timeToOpen > 0)
        {
            _currentTimeToOpen = timeToOpen;
        }
        firstState = true;
        isOpen = false;
    }

    public void Toggle()
    {
        if (isOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    public void StopTimers()
    {
        timeToOpen = 0;
        timeToClose = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isUnlocked)
        {
            Open();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (closeAfterPass)
        {
            Close();
        }
    }
}
