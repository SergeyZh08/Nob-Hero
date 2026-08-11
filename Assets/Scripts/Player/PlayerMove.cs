using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public event Action<bool> OnRunStarting;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Rigidbody _rigidbody;
    public Player Player { get; private set; }
    private bool _isRun  = false;
    private bool _oldState = false;
    
    private Vector2 _movement;

    public void Init(Player player)
    {
        Player = player;
    }

    private void Update()
    {
        _movement = _joystick.Value;
    }

    private void FixedUpdate()
    {
        Vector3 speedVector = _speed * (1 + Player.Stats.MovementSpeed) * new Vector3(_movement.x, 0, _movement.y); 
        _rigidbody.linearVelocity = speedVector;

        if (_rigidbody.linearVelocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.linearVelocity, Vector3.up);
            _isRun = true;
        }
        else
        {
            _isRun = false;
        }

        if (_isRun != _oldState)
        {
            _oldState = _isRun;
            OnRunStarting?.Invoke(_isRun);
        }
    }

}
