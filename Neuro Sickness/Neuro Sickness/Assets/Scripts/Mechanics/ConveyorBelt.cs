using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public enum moveType
    {
        downTop,
        diagonalUp,
        leftRight,
        diagonalDown
    }
    public string invertConveyorCode, reverseConveyorCode, turnOnConveyorCode, turnOffConveyorCode;
    public moveType direction;
    public float speed;
    public bool inverted, on;
    public Vector3Variable movementChanger;
    public StringVariable interactionCode;

    private List<Rigidbody2D> objOnConveyor = new List<Rigidbody2D>();
    private bool playerOnArea;
    private float currentSpeed;

    private Animator anim;

    private void Start()
    {
        GetComponent<BoxCollider2D>().size = GetComponent<SpriteRenderer>().size;
        anim = GetComponent<Animator>();
    }

    public void Interacted()
    {
        if (interactionCode.Value == invertConveyorCode)
        {
            Invert();
        }
        else if (interactionCode.Value == reverseConveyorCode)
        {
            Reverse();
        }
        else if (interactionCode.Value == turnOnConveyorCode)
        {
            TurnOn();
        }
        else if (interactionCode.Value == turnOffConveyorCode)
        {
            TurnOff();
        }
    }

    private void FixedUpdate()
    {
        if (on)
        {
            Movement();
        }
    }

    public void Movement()
    {
        currentSpeed = inverted ? -speed : speed;
        switch (direction) 
        {
            case moveType.downTop:
                if (playerOnArea)
                {
                    movementChanger.Value += Vector3.up * currentSpeed * Time.deltaTime;
                }
                for (int i = 0; i < objOnConveyor.Count; i++)
                {
                    objOnConveyor[i].velocity = Vector3.up * currentSpeed * Time.deltaTime;
                }
                break;
            case moveType.diagonalUp:
                if (playerOnArea)
                {
                    movementChanger.Value += new Vector3(Mathf.Sqrt(2) * currentSpeed * Time.deltaTime, Mathf.Sqrt(2) * currentSpeed * Time.deltaTime);
                }
                for (int i = 0; i < objOnConveyor.Count; i++)
                {
                    objOnConveyor[i].velocity = new Vector3(Mathf.Sqrt(2) * currentSpeed * Time.deltaTime, Mathf.Sqrt(2) * currentSpeed * Time.deltaTime);
                }
                break;
            case moveType.leftRight:
                if (playerOnArea)
                {
                    movementChanger.Value += Vector3.right * currentSpeed * Time.deltaTime;
                }
                for (int i = 0; i < objOnConveyor.Count; i++)
                {
                    objOnConveyor[i].velocity = Vector3.right * currentSpeed * Time.deltaTime;
                }
                break;
            case moveType.diagonalDown:
                if (playerOnArea)
                {
                    movementChanger.Value += new Vector3(Mathf.Sqrt(2) * currentSpeed * Time.deltaTime, Mathf.Sqrt(2) * currentSpeed * -Time.deltaTime);
                }
                for (int i = 0; i < objOnConveyor.Count; i++)
                {
                    objOnConveyor[i].velocity = new Vector3(Mathf.Sqrt(2) * currentSpeed * Time.deltaTime, Mathf.Sqrt(2) * currentSpeed * -Time.deltaTime);
                }
                break;
            default:
                break;
        }
    }

    public void TurnOn()
    {
        on = true;
    }

    public void TurnOff()
    {
        for (int i = 0; i < objOnConveyor.Count; i++)
        {
            objOnConveyor[i].velocity = Vector3.zero;
        }
        on = false;
    }

    public void Invert()
    {
        inverted = true;
        anim.SetFloat("Direction", -1);
    }

    public void Reverse()
    {
        inverted = false;
        anim.SetFloat("Direction", 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerOnArea = true;
        }
        else
        {
            Debug.Log("não player");
        }
        if (collision.CompareTag("Collectable"))
        {
            Debug.Log("colecionavel");
            objOnConveyor.Add(collision.GetComponent<Rigidbody2D>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerOnArea = false;
        }
        if (collision.CompareTag("Collectable"))
        {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            objOnConveyor.Remove(collision.GetComponent<Rigidbody2D>());
        }
    }
}
