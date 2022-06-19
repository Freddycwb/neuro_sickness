using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public bool loreDialog;
    public string[] speekCode;
    public string dialog;
    public AIDialogs aI;
    public StringVariable interactionCode;

    public void Interacted()
    {
        for (int i = 0; i < speekCode.Length; i++)
        {
            if (interactionCode.Value == speekCode[i])
            {
                if (!loreDialog)
                {
                    AISpeek();
                }
                else
                {
                    LoreInteractableSpeek();
                }
            }
        }
    }

    public void AISpeek()
    {

    }

    public void LoreInteractableSpeek()
    {

    }
}
