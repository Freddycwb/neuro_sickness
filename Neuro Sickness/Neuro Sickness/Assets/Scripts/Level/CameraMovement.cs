using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObjectVariable player;

    void Update()
    {
        if (player.Value != null)
        {
            float x = Mathf.FloorToInt((player.Value.transform.position.x + 9.75f) / 19.5f) * 19.5f;
            float y = Mathf.FloorToInt((player.Value.transform.position.y + 5.5f) / 11) * 11;
            transform.position = new Vector3(x, y, -10);
        }
    }
}
