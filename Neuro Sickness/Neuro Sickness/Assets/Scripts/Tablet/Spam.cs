using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spam : MonoBehaviour
{
    public FloatVariable difficulty;
    public string currentSymbol;
    public TextMeshPro symbol;
    public GameObject bar;
    public float count;
    public bool minigameIsOver;
    public GameEvent hackCompleted;

    private string st = "BCEFGHIJKLMNOPRTUVXYZ";

    private void OnEnable()
    {
        ChangeSymbol();
        bar.transform.localScale = new Vector3(bar.transform.localScale.x, 0, bar.transform.localScale.z);
        minigameIsOver = false;
    }

    private void Update()
    {
        TimerToChange();
    }

    private void OnGUI()
    {
        if (Input.anyKeyDown)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                if (e.keyCode.ToString() == currentSymbol && bar.transform.localScale.y < 5)
                {
                    bar.transform.localScale = new Vector3(bar.transform.localScale.x, bar.transform.localScale.y + (0.5f / (difficulty.Value / 2.5f)), bar.transform.localScale.z);
                } 
                else if (e.keyCode.ToString() == currentSymbol && bar.transform.localScale.y >= 5)
                {
                    CompleteMinigame();
                }
            }
        }
    }
    
    public void TimerToChange()
    {
        if (!minigameIsOver)
        {
            count -= Time.deltaTime;
            if (count <= 0)
            {
                ChangeSymbol();
            }
        }
    }

    public void ChangeSymbol()
    {
        char c = st[Random.Range(0,st.Length)];
        currentSymbol = c.ToString();
        symbol.text = currentSymbol;
        count = 4 / ((difficulty.Value + 5.5f) / 4);
    }

    public void CompleteMinigame()
    {
        minigameIsOver = true;
        hackCompleted.Raise();
        gameObject.SetActive(false);
    }

    public void CancelMinigame()
    {
        minigameIsOver = true;
        gameObject.SetActive(false);
    }
}
