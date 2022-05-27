using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogDetector : MonoBehaviour
{
    public GameObjectVariable player;
    public float timeStopped;
    public bool talking;

    private void Update()
    {
        MuchTimeStopped();
    }

    private void MuchTimeStopped()
    {
        if (player.Value.GetComponent<Rigidbody2D>().velocity.magnitude > 0)
        {
            timeStopped = 0;
        }
        else if(player.Value.GetComponent<Rigidbody2D>().velocity.magnitude <= 0 && !talking)
        {
            timeStopped += Time.deltaTime;
        }
        if (timeStopped > 10 && timeStopped < 11 && !talking)
        {
            GetComponent<AIDialogs>().Speech("Usuario, lembre - se que voce pode se movimentar utilizando as teclas W A S D");
            timeStopped += 2;
        }
        if (timeStopped > 20 && timeStopped < 21 && !talking)
        {
            GetComponent<AIDialogs>().Speech("A area aparenta estar segura, me silenciarei enquanto o Usuario faz uma pausa");
            timeStopped += 2;
        }
    }

    public void AIStartTalk()
    {
        talking = true;
    }

    public void AIStopTalk()
    {
        talking = false;
    }
}
