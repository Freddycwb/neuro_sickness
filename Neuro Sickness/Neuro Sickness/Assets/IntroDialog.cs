using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroDialog : MonoBehaviour
{
    public string dialog;

    public GameEvent aIStopTalk;

    void Start()
    {
        StartCoroutine(TimeToStartText());
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            aIStopTalk.Raise();
        }
    }

    private IEnumerator TimeToStartText()
    {
        yield return new WaitForSeconds(1);
        GetComponent<AIDialogs>().Speech(dialog);
    }
}
