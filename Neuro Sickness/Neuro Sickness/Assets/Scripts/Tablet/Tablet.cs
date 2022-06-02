using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tablet : MonoBehaviour
{
    public float speed = 1;
    public StringVariable minigameRequested;
    public GameObject[] minigames;

    public void CallTablet()
    {
        StopAllCoroutines();
        StartCoroutine(MoveTablet(true));
    }

    public void CloseTablet()
    {
        StopAllCoroutines();
        StartCoroutine(MoveTablet(false));
    }

    public IEnumerator MoveTablet(bool show)
    {
        float y = transform.position.y;
        if (show) {
            while (y < transform.parent.position.y)
            {
                transform.position = new Vector3(transform.position.x, y, transform.position.z);
                y += Time.deltaTime * speed;
                yield return new WaitForEndOfFrame();
            }
            CallMinigame();
        }
        else
        {
            while (y > transform.parent.position.y - 12)
            {
                transform.position = new Vector3(transform.position.x, y, transform.position.z);
                y -= Time.deltaTime * speed;
                yield return new WaitForEndOfFrame();
            }
        }
    }

    public void CallMinigame()
    {
        if (minigameRequested.Value == "Spam")
        {
            minigames[0].SetActive(true);
        }
        if (minigameRequested.Value == "Osu")
        {
            minigames[1].SetActive(true);
        }
    }
}
