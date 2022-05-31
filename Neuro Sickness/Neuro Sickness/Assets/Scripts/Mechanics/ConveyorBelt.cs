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
    public moveType direction;
    public float speed;
    public string[] invertConveyorCode, reverseConveyorCode, turnOnConveyorCode, turnOffConveyorCode, turnConveyorDirectionCode, ChangeConveyorStateCode;
    public float timeToInvert, timeToReverse, timeToTurnOn, timeToTurnOff;

    public bool inverted, on;
    public Vector3Variable movementChanger;
    public StringVariable interactionCode;

    private List<Rigidbody2D> _objOnConveyor = new List<Rigidbody2D>();
    private bool _playerOnArea;
    private float _currentSpeed;
    private float _currentTimeToInvert, _currentTimeToReverse, _currentTimeToTurnOn, _currentTimeToTurnOff;

    private Animator anim;

    private void Start()
    {
        GetComponent<BoxCollider2D>().size = GetComponent<SpriteRenderer>().size;
        anim = GetComponent<Animator>();
        if (inverted)
        {
            Invert();
        }
        else
        {
            Reverse();
        }
        if (on)
        {
            TurnOn();
        }
        else
        {
            TurnOff();
        }
    }

    public void Interacted()
    {
        for (int i = 0; i < invertConveyorCode.Length; i++)
        {
            if (interactionCode.Value == invertConveyorCode[i])
            {
                Invert();
            }
        }
        for (int i = 0; i < reverseConveyorCode.Length; i++)
        {
            if (interactionCode.Value == reverseConveyorCode[i])
            {
                Reverse();
            }
        }
        for (int i = 0; i < turnOnConveyorCode.Length; i++)
        {
            if (interactionCode.Value == turnOnConveyorCode[i])
            {
                TurnOn();
            }
        }
        for (int i = 0; i < turnOffConveyorCode.Length; i++)
        {
            if (interactionCode.Value == turnOffConveyorCode[i])
            {
                TurnOff();
            }
        }
        for (int i = 0; i < turnConveyorDirectionCode.Length; i++)
        {
            if (interactionCode.Value == turnConveyorDirectionCode[i])
            {
                TurnConveyorDirection();
            }
        }
        for (int i = 0; i < ChangeConveyorStateCode.Length; i++)
        {
            if (interactionCode.Value == ChangeConveyorStateCode[i])
            {
                ChangeConveyorState();
            }
        }
    }

    private void FixedUpdate()
    {
        if (on)
        {
            Movement();
        }
    }

    private void Update()
    {
        ConveyorTimer();
    }

    public void Movement()
    {
        _currentSpeed = inverted ? -speed : speed;
        switch (direction) 
        {
            case moveType.downTop:
                if (_playerOnArea)
                {
                    movementChanger.Value += Vector3.up * _currentSpeed * Time.deltaTime;
                }
                for (int i = 0; i < _objOnConveyor.Count; i++)
                {
                    _objOnConveyor[i].velocity = Vector3.up * _currentSpeed * Time.deltaTime;
                }
                break;
            case moveType.diagonalUp:
                if (_playerOnArea)
                {
                    movementChanger.Value += new Vector3(Mathf.Sqrt(2) * _currentSpeed * Time.deltaTime, Mathf.Sqrt(2) * _currentSpeed * Time.deltaTime);
                }
                for (int i = 0; i < _objOnConveyor.Count; i++)
                {
                    _objOnConveyor[i].velocity = new Vector3(Mathf.Sqrt(2) * _currentSpeed * Time.deltaTime, Mathf.Sqrt(2) * _currentSpeed * Time.deltaTime);
                }
                break;
            case moveType.leftRight:
                if (_playerOnArea)
                {
                    movementChanger.Value += Vector3.right * _currentSpeed * Time.deltaTime;
                }
                for (int i = 0; i < _objOnConveyor.Count; i++)
                {
                    _objOnConveyor[i].velocity = Vector3.right * _currentSpeed * Time.deltaTime;
                }
                break;
            case moveType.diagonalDown:
                if (_playerOnArea)
                {
                    movementChanger.Value += new Vector3(Mathf.Sqrt(2) * _currentSpeed * Time.deltaTime, Mathf.Sqrt(2) * _currentSpeed * -Time.deltaTime);
                }
                for (int i = 0; i < _objOnConveyor.Count; i++)
                {
                    _objOnConveyor[i].velocity = new Vector3(Mathf.Sqrt(2) * _currentSpeed * Time.deltaTime, Mathf.Sqrt(2) * _currentSpeed * -Time.deltaTime);
                }
                break;
            default:
                break;
        }
    }

    public void ConveyorTimer()
    {
        if (_currentTimeToInvert > 0 && timeToInvert > 0)
        {
            _currentTimeToInvert -= Time.deltaTime;
            if (_currentTimeToInvert <= 0)
            {
                Invert();
                _currentTimeToInvert = 0;
            }
        }
        if (_currentTimeToReverse > 0 && timeToReverse > 0)
        {
            _currentTimeToReverse -= Time.deltaTime;
            if (_currentTimeToReverse <= 0)
            {
                Reverse();
                _currentTimeToReverse = 0;
            }
        }
        if (_currentTimeToTurnOn > 0 && timeToTurnOn > 0)
        {
            _currentTimeToTurnOn -= Time.deltaTime;
            if (_currentTimeToTurnOn <= 0)
            {
                TurnOn();
                _currentTimeToTurnOn = 0;
            }
        }
        if (_currentTimeToTurnOff > 0 && timeToTurnOff > 0)
        {
            _currentTimeToTurnOff -= Time.deltaTime;
            if (_currentTimeToTurnOff <= 0)
            {
                TurnOff();
                _currentTimeToTurnOff = 0;
            }
        }
    }

    public void Invert()
    {
        inverted = true;
        anim.SetFloat("Direction", -1);
        if (timeToReverse > 0)
        {
            _currentTimeToReverse = timeToReverse;
        }
    }

    public void Reverse()
    {
        inverted = false;
        anim.SetFloat("Direction", 1);
        if (timeToInvert > 0)
        {
            _currentTimeToInvert = timeToInvert;
        }
    }

    public void TurnOn()
    {
        on = true;
        if (timeToTurnOff > 0)
        {
            _currentTimeToTurnOff = timeToTurnOff;
        }
    }

    public void TurnOff()
    {
        for (int i = 0; i < _objOnConveyor.Count; i++)
        {
            _objOnConveyor[i].velocity = Vector3.zero;
        }
        on = false;
        if (timeToTurnOn > 0)
        {
            _currentTimeToTurnOn = timeToTurnOn;
        }
    }

    public void TurnConveyorDirection()
    {
        if (inverted)
        {
            Reverse();
        }
        else
        {
            Invert();
        }
    }

    public void ChangeConveyorState()
    {
        if (on)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerOnArea = true;
        }
        else
        {
            Debug.Log("não player");
        }
        if (collision.CompareTag("Collectable"))
        {
            Debug.Log("colecionavel");
            _objOnConveyor.Add(collision.GetComponent<Rigidbody2D>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerOnArea = false;
        }
        if (collision.CompareTag("Collectable"))
        {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            _objOnConveyor.Remove(collision.GetComponent<Rigidbody2D>());
        }
    }
}
