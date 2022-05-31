using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetVariables : MonoBehaviour
{
    public Vector3Variable movementChanger;
    public StringVariable interactionCode;
    public BoolVariable canControl;
    public StringVariable minigameRequest;
    public FloatVariable minigameDifficulty;
    public IntVariable itemInHand;
    public GameObjectArrayVariable inventory;
    public GameObjectVariable collectable;

    private void Start()
    {
        interactionCode.Value = "";
        canControl.Value = true;
        minigameRequest.Value = "";
        minigameDifficulty.Value = 1;
        itemInHand.Value = 0;
        for (int i = 0; i < inventory.Value.Length; i++)
        {
            inventory.Value[i] = null;
        }
        collectable.Value = null;
    }
}
