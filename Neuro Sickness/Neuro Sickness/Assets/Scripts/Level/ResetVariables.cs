using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObjectArrayVariable inventory;
    public GameObjectVariable collectable;

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
    }
}
