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
    public SoundVariable btnPressedSound;

    private bool playerInArea = false, on;
    private Animator _animatorFront, _animatorSide;
    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponentInChildren<AudioSource>();
        if (!isTriggerArea)
        {
            _animatorFront = transform.GetChild(0).GetChild(0).GetComponent<Animator>();
            _animatorSide = transform.GetChild(0).GetChild(1).GetComponent<Animator>();
            _animatorFront.Play("Click", -1, 0f);
            _animatorSide.Play("ClickSide", -1, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInArea = true;
            if (isTriggerArea)
            {
                Interact();
            }
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
            if (!isTriggerArea) 
            {
                if (ItemRequested == "")
                {
                    _audio.clip = btnPressedSound.Value;
                    _audio.Play();
                }
                _animatorFront.Play("Click", -1, 0f);
                _animatorSide.Play("ClickSide", -1, 0f);
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
