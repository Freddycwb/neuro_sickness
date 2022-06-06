using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedVariables : MonoBehaviour
{
    public Vector3Variable playerRespawn;
    public GameObjectVariable player;

    public void Save()
    {
        playerRespawn.Value = player.Value.transform.position;

    }
}
