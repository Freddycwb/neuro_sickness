using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetVariables : MonoBehaviour
{
    public SpriteRenderer transition;
    public float transitionSpeed;

    public IntVariable itemInHand;
    public IntVariable health;
    public IntVariable maxHealth;
    public IntVariable maxStamina;

    public FloatVariable minigameDifficulty;
    public FloatVariable stamina;

    public BoolVariable canControl;

    public StringVariable minigameRequest;
    public StringVariable interactionCode;
    public StringVariable sceneToLoad;

    public Vector3Variable movementChanger;
    public Vector3Variable respawnPosition;

    public GameObjectArrayVariable inventory;
    public GameObjectVariable collectable;
    public GameObjectVariable player;

    public SoundVariable music;

    private AudioSource _audio;

    private bool _fadeOut, _fadeIn;

    private void Start()
    {
        itemInHand.Value = 0;
        health.Value = maxHealth.Value;

        minigameDifficulty.Value = 1;
        stamina.Value = maxStamina.Value;

        canControl.Value = true;

        minigameRequest.Value = "";
        interactionCode.Value = "";
        sceneToLoad.Value = "FinalStage";

        movementChanger.Value = Vector3.zero;

        for (int i = 0; i < inventory.Value.Length; i++)
        {
            inventory.Value[i] = null;
        }
        collectable.Value = null;
        _audio = GetComponent<AudioSource>();
        _audio.clip = music.Value;
        _audio.Play();
        StartFadeOut();
    }

    private void Update()
    {
        FadeOut();
        FadeIn();
    }

    public void StartFadeOut()
    {
        _fadeOut = true;
    }

    public void FadeOut()
    {
        if (_fadeOut)
        {
            transition.color = new Color(transition.color.r, transition.color.g, transition.color.b, transition.color.a - Time.deltaTime * transitionSpeed);
            if (transition.color.a <= 0)
            {
                transition.color = new Color(transition.color.r, transition.color.g, transition.color.b, 0);
                _fadeOut = false;
            }
        }
    }

    public void StartFadeIn()
    {
        _fadeIn = true;
    }

    public void FadeIn()
    {
        if (_fadeIn)
        {
            transition.color = new Color(transition.color.r, transition.color.g, transition.color.b, transition.color.a + Time.deltaTime * transitionSpeed);
            if (transition.color.a >= 1)
            {
                transition.color = new Color(transition.color.r, transition.color.g, transition.color.b, 1);
                SceneManager.LoadScene(sceneToLoad.Value);
                _fadeIn = false;
            }
        }
    }

    public void Respawn()
    {
        sceneToLoad.Value = "FinalStage";
        StartCoroutine(CallFadeIn());
    }

    public void End()
    {
        sceneToLoad.Value = "End";
        StartCoroutine(CallFadeIn());
    }

    public void StartGame()
    {
        sceneToLoad.Value = "Intro";
        StartFadeIn();
    }


    private IEnumerator CallFadeIn()
    {
        yield return new WaitForSeconds(2);
        StartFadeIn();
    }
}
