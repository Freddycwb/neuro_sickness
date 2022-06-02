using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OsuElement : MonoBehaviour
{
    private Osu osu;


    private void Start()
    {
        osu = transform.parent.GetComponentInParent<Osu>();
    }

    private void OnMouseDown()
    {
        if (osu.currentNumber.ToString() == transform.parent.GetChild(1).GetComponent<TextMeshPro>().text)
        {
            osu.ElementClicked();
            Destroy(transform.parent.gameObject);
        }
    }
}
