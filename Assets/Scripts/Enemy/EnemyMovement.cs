using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _accelerationSmoothness = 10f;
    public Enemy Enemy { get; private set; }
    [SerializeField] private float _speed;
    [SerializeField] private float _angularSpeed;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _distanceToCarry = 30f;
    private Transform _target;
    private List<ISpeedModifier> _speedModifiers = new List<ISpeedModifier>();
    private float _modifierSpeed = 1f;
    private float _sqrDistanceToCarry;

    public void Init(Enemy enemy)
    {
        Enemy = enemy;
        _sqrDistanceToCarry = Mathf.Pow(_distanceToCarry, 2);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void FixedUpdate()
    {
         Vector3 toTarget = _target.position - transform.position;

         Quaternion rotation = Quaternion.LookRotation(toTarget);

         _rigidbody.MoveRotation(Quaternion.Lerp(_rigidbody.rotation,rotation,Time.fixedDeltaTime * _angularSpeed));

        //если враг вышел за круг, телепортируем в другу сторону
        if (toTarget.sqrMagnitude > _sqrDistanceToCarry)
        {
           transform.position += toTarget * 1.95f;
        }

        Vector3 desiredVelocity = _speed * _modifierSpeed * transform.forward;
        Vector3 currentVelocity = _rigidbody.linearVelocity;
        Vector3 finalVelocity = Vector3.Lerp(currentVelocity, desiredVelocity, Time.fixedDeltaTime * _accelerationSmoothness);

        _rigidbody.linearVelocity = finalVelocity;
    }


    public void AddModifier(ISpeedModifier modifier)
    {
        _speedModifiers.Add(modifier);
        CalculateModifierSpeed();
    }

    public void RemoveModifier(ISpeedModifier modifier)
    {
        _speedModifiers.Remove(modifier);
        CalculateModifierSpeed();
    }

    private void CalculateModifierSpeed()
    {
        _modifierSpeed = 1f;

        foreach (var m in _speedModifiers)
        {
            _modifierSpeed = m.ModifySpeed(_modifierSpeed);
        }
    }

    private void OnDisable()
    {
        _target = null;
        _rigidbody.linearVelocity = Vector3.zero;
    }
}
