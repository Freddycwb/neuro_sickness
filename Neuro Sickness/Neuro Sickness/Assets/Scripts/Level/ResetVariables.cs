using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetVariables : MonoBehaviour
{
    public IntVariable itemInHand;
    public IntVariable health;
    public IntVariable maxHealth;
    public IntVariable maxStamina;

    public FloatVariable minigameDifficulty;
    public FloatVariable stamina;

    public BoolVariable canControl;

    public StringVariable minigameRequest;
    public StringVariable interactionCode;

    public Vector3Variable movementChanger;
    public Vector3Variable respawnPosition;

    public GameObjectArrayVariable inventory;
    public GameObjectVariable collectable;
    public GameObjectVariable player;

    public SoundVariable music;

    private AudioSource _audio;

    private void Start()
    {
        itemInHand.Value = 0;
        health.Value = maxHealth.Value;

        minigameDifficulty.Value = 1;
        stamina.Value = maxStamina.Value;

        canControl.Value = true;

        minigameRequest.Value = "";
        interactionCode.Value = "";

        movementChanger.Value = Vector3.zero;

        for (int i = 0; i < inventory.Value.Length; i++)
        {
            inventory.Value[i] = null;
        }
        collectable.Value = null;
        _audio = GetComponent<AudioSource>();
        _audio.clip = music.Value;
        _audio.Play();
    }

    public void Respawn()
    {
        SceneManager.LoadScene("Station2");
    }
}
