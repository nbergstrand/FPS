using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{

   
    [SerializeField]
    EnemyState _state;

    Transform _targetTransorm;
    IDamageable _target;

    CharacterController _controller;

    [SerializeField]
    float _speed;

    [SerializeField]
    float _rotationSpeed;

    [SerializeField]
    int _maxHealth;

    [SerializeField]
    float _attackCooldown;

    float _attackCooldownTimer;

    public int Health { get; set; }

    float _verticalVelocity;
    float _gravity = 10;

    
    void Start()
    {
        Health = _maxHealth;

        _targetTransorm = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _target = _targetTransorm.GetComponent<IDamageable>();

        _controller = GetComponent<CharacterController>();

        if (_targetTransorm == null)
        {
            Debug.LogError("No player found");
        }
    }

    void Update()
    {
       if(_state == EnemyState.Chase)
       {
            ChaseTarget();
            
       }

       if(_state == EnemyState.Chase || _state == EnemyState.Attack)
        {
            FaceTarget();
        }


        if (_state == EnemyState.Attack)
        {
            AttackPlayer();

        }

    }

    public void SetState(EnemyState state)
    {
        _state = state;
    }

    void AttackPlayer()
    {

        if (_attackCooldownTimer <= 0)
        {
            _attackCooldownTimer = _attackCooldown;
            _target.Damage(10);

        }

        _attackCooldownTimer -= Time.deltaTime;
    }

   void ChaseTarget()
    {
        Vector3 direction =(_targetTransorm.position - transform.position).normalized;

        if(_controller.isGrounded == false)
        {
            _verticalVelocity -= _gravity * Time.deltaTime;
           
        }

        direction.y =_verticalVelocity;

        _controller.Move(direction * Time.deltaTime * _speed);
    }

    void FaceTarget()
    {
        Vector3 direction = (_targetTransorm.position - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);

    }

    public void Damage(int damageAmount)
    {
        Health -= damageAmount; 

        if(Health <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        Destroy(gameObject);
    }

}

public enum EnemyState
{
    Idle,
    Chase,
    Attack
}

