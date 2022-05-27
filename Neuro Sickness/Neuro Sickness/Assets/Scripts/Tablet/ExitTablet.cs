using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTablet : MonoBehaviour
{
    public GameEvent cancelHack;

    private void OnMouseDown()
    {
        cancelHack.Raise();
    }
}
