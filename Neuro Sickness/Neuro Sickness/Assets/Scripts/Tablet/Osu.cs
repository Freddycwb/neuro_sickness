using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Osu : MonoBehaviour
{
    public FloatVariable difficulty;
    public GameObject osuElement;
    public int currentNumber;
    public bool minigameIsOver;
    public GameEvent hackCompleted;

    private List<Vector2> elementsPositions = new List<Vector2>();

    private void OnEnable()
    {
        currentNumber = 1;
        elementsPositions.Clear();
        minigameIsOver = false;
        SpawnElements();
    }

    public void SpawnElements()
    {
        for (int i = 0; i < 4 + difficulty.Value; i++)
        {
            int x = Random.RandomRange(-4, 5);
            int y = Random.RandomRange(-2, 1);
            for (int c = 0; c < elementsPositions.Count; c++)
            {
                if (new Vector2(x, y) == elementsPositions[c])
                {
                    x = Random.RandomRange(-4, 5);
                    y = Random.RandomRange(-2, 1);
                    c = 0;
                }
            }
            elementsPositions.Add(new Vector2(x, y));
            var a = Instantiate(osuElement, transform.position, transform.rotation);
            a.transform.SetParent(gameObject.transform.GetChild(0));
            a.transform.localPosition = new Vector2(x,y);
            a.transform.GetComponentInChildren<TextMeshPro>().text = (i + 1).ToString();
        }
    }

    public void ElementClicked()
    {
        currentNumber += 1;
        if (currentNumber > 4 + difficulty.Value)
        {
            CompleteMinigame();
        }
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
        if (transform.GetChild(0).childCount > 0)
        {
            for (int i = transform.GetChild(0).childCount - 1; i >= 0; i--)
            {
                Destroy(transform.GetChild(0).GetChild(i).gameObject);
            }
        }
        gameObject.SetActive(false);
    }
}
