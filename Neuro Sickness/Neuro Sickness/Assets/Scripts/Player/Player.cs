using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float stunTime;
    public GameObjectVariable player;
    public GameObjectVariable collectable;
    public BoolVariable canControl;
    public Vector3Variable movementChanger;
    public BoolVariable isRunning;
    public BoolVariable holdingRun;
    public SoundVariable stepSound, damageSound;

    public GameEvent actionButton;
    public GameEvent dropButton;
    public GameEvent hackCancel;
    public GameEvent takeDamage;

    private Rigidbody2D _rb;
    private Animator _animator;
    private float _velocityX, _velocityY;
    private float _currentStunTime;
    private string _currentState;
    private AudioSource _audio;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        player.Value = gameObject;
    }

    private void FixedUpdate()
    {
        if (_currentStunTime <= 0)
        {
            _rb.velocity = new Vector2(_velocityX + movementChanger.Value.x, _velocityY + movementChanger.Value.y);
        }
        else
        {
            _currentStunTime -= Time.deltaTime;
            _rb.velocity = new Vector2(_rb.velocity.x / 1.1f, _rb.velocity.y / 1.1f);
            if (_currentStunTime <= 0)
            {
                _currentStunTime = 0;
                canControl.Value = true;
            }
        }
    }

    void Update()
    {
        if (canControl.Value)
        {
            Move();
            Interact();
            Drop();
        }
        PlayAnimation();
    }
    private void Move()
    {
        holdingRun.Value = Input.GetKey(KeyCode.LeftShift) && (_velocityX != 0 || _velocityY != 0);
        if (Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") != 0)
        {
            _velocityX = isRunning.Value ? (Input.GetAxis("Horizontal") / Mathf.Sqrt(2)) * speed * 1.5f : (Input.GetAxis("Horizontal") / Mathf.Sqrt(2)) * speed;
            _velocityY = isRunning.Value ? (Input.GetAxis("Vertical") / Mathf.Sqrt(2)) * speed * 1.5f : (Input.GetAxis("Vertical") / Mathf.Sqrt(2)) * speed;
        }
        else
        {
            _velocityX = isRunning.Value ? Input.GetAxis("Horizontal") * speed * 2 : Input.GetAxis("Horizontal") * speed;
            _velocityY = isRunning.Value ? Input.GetAxis("Vertical") * speed * 2 : Input.GetAxis("Vertical") * speed;
        }
    }

    void Interact()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            actionButton.Raise();
        }
    }

    void Drop()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            dropButton.Raise();
        }
    }

    private void PlayAnimation()
    {
        if (_currentStunTime <= 0)
        {
            if (_rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (_rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            if (new Vector2(_velocityX, _velocityY).magnitude > 1)
            {
                ChangeAnimatorState("PlayerWalk");
            }
            if (new Vector2(_velocityX, _velocityY).magnitude == 0)
            {
                ChangeAnimatorState("PlayerIdle");
            }
            if (isRunning.Value)
            {
                _animator.speed = 1.5f;
            }
            else
            {
                _animator.speed = 1;
            }
        }
        if (_currentStunTime > 0)
        {
            ChangeAnimatorState("PlayerHurt");
        }
    }

    public void LoseControl()
    {
        canControl.Value = false;
        _rb.velocity = Vector3.zero;
        _velocityX = 0;
        _velocityY = 0;
    }

    public void TakeControl()
    {
        canControl.Value = true;
    }

    public void TakeDamage()
    {
        if (_currentStunTime == 0)
        {
            _audio.clip = damageSound.Value;
            _audio.Play();
            takeDamage.Raise();
        }
    }

    public void Knockback(Vector2 knockPos, float knockForce)
    {
        _currentStunTime = stunTime;
        canControl.Value = false;
        _rb.velocity = Vector3.zero;
        _velocityX = 0;
        _velocityY = 0;
        Vector2 heading = (transform.position + new Vector3(0, 0.2f)) - (Vector3)knockPos;
        Vector2 normalDirection = heading.normalized;
        _rb.AddForce(normalDirection * ((knockForce * 7)), ForceMode2D.Impulse);
    }

    public void Death()
    {
        canControl.Value = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable")
        {
            collectable.Value = collision.gameObject;
        }
        if (collision.gameObject.tag == "Finish")
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Collectable")
        {
            collectable.Value = null;
        }
        if (collision.gameObject.GetComponent<ConveyorBelt>() != null)
        {
            movementChanger.Value = Vector3.zero;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Damage")
        {
            TakeDamage();
            Knockback(collision.GetContact(0).point, 1);
        }
    }

    void ChangeAnimatorState(string newState)
    {
        if (_currentState == newState) return;
        _animator.Play(newState);
        _currentState = newState;
    }

    public void StepSound()
    {
        _audio.clip = stepSound.Value;
        _audio.Play();
    }
}
