using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObjectVariable player;

    void Update()
    {
        float x = Mathf.FloorToInt((player.Value.transform.position.x + 8.885f) / 17.77f) * 17.77f;
        float y = Mathf.FloorToInt((player.Value.transform.position.y + 5) / 10) * 10;
        transform.position = new Vector3(x, y, -10);
    }
}
