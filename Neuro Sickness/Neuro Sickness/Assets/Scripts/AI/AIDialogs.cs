using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AIDialogs : MonoBehaviour
{
    public float delayBetweenLetters;
    public float delayToHideText;

    public GameEvent aiStopTalk;
    public GameEvent aiStartTalk;

    public void Speech(string text)
    {
        aiStartTalk.Raise();
        StopAllCoroutines();
        GetComponentInChildren<TextMeshPro>().text = "";
        StartCoroutine(Talk(text));
    }

    private IEnumerator Talk(string text)
    {
        foreach (char letter in text.ToCharArray())
        {
            if (letter != '.')
            {
                GetComponentInChildren<TextMeshPro>().text += letter;
                yield return new WaitForSeconds(delayBetweenLetters);
            }
            else
            {
                yield return new WaitForSeconds(delayToHideText);
                GetComponentInChildren<TextMeshPro>().text = "";
            }
        }
        yield return new WaitForSeconds(delayToHideText);
        GetComponentInChildren<TextMeshPro>().text = "";
        aiStopTalk.Raise();
    }
}
