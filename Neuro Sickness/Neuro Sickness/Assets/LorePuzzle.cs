using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LorePuzzle : MonoBehaviour
{
    public string[] toggle1Code, toggle2Code, toggle3Code, toggle4Code, toggle5Code;
    public bool password1, password2, password3, password4, password5;
    public Door door;
    public Sprite on, off;
    public StringVariable interactionCode;

    private bool toggle1, toggle2, toggle3, toggle4, toggle5;

    public void Interacted()
    {
        for (int i = 0; i < toggle1Code.Length; i++)
        {
            if (interactionCode.Value == toggle1Code[i])
            {
                Toggle1();
            }
        }
        for (int i = 0; i < toggle2Code.Length; i++)
        {
            if (interactionCode.Value == toggle2Code[i])
            {
                Toggle2();
            }
        }
        for (int i = 0; i < toggle3Code.Length; i++)
        {
            if (interactionCode.Value == toggle3Code[i])
            {
                Toggle3();
            }
        }
        for (int i = 0; i < toggle4Code.Length; i++)
        {
            if (interactionCode.Value == toggle4Code[i])
            {
                Toggle4();
            }
        }
        for (int i = 0; i < toggle5Code.Length; i++)
        {
            if (interactionCode.Value == toggle5Code[i])
            {
                Toggle5();
            }
        }
    }

    void Toggle1()
    {
        toggle1 = !toggle1;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = toggle1 ? on : off;
        CorrectPassword(); 
    }

    void Toggle2()
    {
        toggle2 = !toggle2;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = toggle2 ? on : off;
        CorrectPassword();
    }

    void Toggle3()
    {
        toggle3 = !toggle3;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = toggle3 ? on : off;
        CorrectPassword();
    }

    void Toggle4()
    {
        toggle4 = !toggle4;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = toggle4 ? on : off;
        CorrectPassword();
    }

    void Toggle5()
    {
        toggle5 = !toggle3;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = toggle5 ? on : off;
        CorrectPassword();
    }

    void CorrectPassword()
    {
        if (toggle1 == password1 && toggle2 == password2 && toggle3 == password3 && toggle4 == password4 && toggle5 == password5)
        {
            door.Open();
        }
    }
}
