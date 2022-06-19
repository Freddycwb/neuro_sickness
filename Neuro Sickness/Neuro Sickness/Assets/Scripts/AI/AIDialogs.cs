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

    public TextMeshPro textPanel;

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
                textPanel.text += letter;
                yield return new WaitForSeconds(delayBetweenLetters);
            }
            else
            {
                yield return new WaitForSeconds(delayToHideText);
                textPanel.text = "";
            }
        }
        yield return new WaitForSeconds(delayToHideText);
        textPanel.text = "";
        aiStopTalk.Raise();
    }
}
