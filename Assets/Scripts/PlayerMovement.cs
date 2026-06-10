using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private Vector2 _movement;
    private Vector2 _lastMovement;
    private Rigidbody2D _rb;
    private Animator _animator;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        setMovement();
        Animator();
    }

    void setMovement()
    {
        _movement.Set(InputManager.Movement.x, InputManager.Movement.y);
        _rb.linearVelocity = _movement.normalized * _moveSpeed;
    }

    void Animator()
    {
        if (_rb.linearVelocity.magnitude > 0)
        {
            _animator.SetBool("isWalking", true);
            _animator.SetFloat("InputX", _movement.x);
            _animator.SetFloat("InputY", _movement.y);
            _lastMovement = _movement;
        }
        else
        {
            _animator.SetBool("isWalking", false);
            _animator.SetFloat("LastInputX", _lastMovement.x);
            _animator.SetFloat("LastInputY", _lastMovement.y);
        }
    }

    private void OnDisable() {
        _rb.linearVelocity = Vector2.zero;
        _animator.SetBool("isWalking", false);
    }
}
