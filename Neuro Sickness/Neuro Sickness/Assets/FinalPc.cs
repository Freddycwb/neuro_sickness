using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPc : MonoBehaviour
{
    public GameEvent hack;
    public minigamesType minigame;
    public int difficulty;
    public StringVariable minigameRequested;
    public FloatVariable minigameDifficulty;
    public StringVariable interactionCode;
    public GameEvent interact;
    public BoolVariable canControl;
    public SoundVariable HackStartSound, HackSuccessSound;
    public BoxCollider2D end;

    private bool playerInArea;
    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public enum minigamesType
    {
        Spam,
        Osu
    }

    private bool beingHacked, on;

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

    private void OnMouseDown()
    {
        if (canControl.Value)
        {
            minigameRequested.Value = minigame.ToString();
            minigameDifficulty.Value = difficulty;
            beingHacked = true;
            _audio.clip = HackStartSound.Value;
            _audio.Play();
            hack.Raise();
        }
    }

    public void HackManual()
    {
        if (canControl.Value && playerInArea)
        {
            minigameRequested.Value = minigame.ToString();
            minigameDifficulty.Value = difficulty;
            beingHacked = true;
            _audio.clip = HackStartSound.Value;
            _audio.Play();
            hack.Raise();
        }
    }

    public void Interact()
    {
        if (beingHacked)
        {
            Debug.Log("aaaaa");
            end.enabled = true;
            GetComponent<Animator>().SetTrigger("Pass");
            interact.Raise();
            _audio.clip = HackSuccessSound.Value;
            _audio.Play();
            beingHacked = false;
        }
    }

    public void CancelHack()
    {
        beingHacked = false;
    }
}
