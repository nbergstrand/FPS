using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{

    enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }

    [SerializeField]
    EnemyState _state;

    Transform _target;
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

        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        _controller = GetComponent<CharacterController>();

        if (_target == null)
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
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _state = EnemyState.Attack;
        }
               

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _state = EnemyState.Chase;
        }


    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (_attackCooldownTimer <= 0)
            {
                _attackCooldownTimer = _attackCooldown;
                other.GetComponent<IDamageable>().Damage(10);

            }

            _attackCooldownTimer -= Time.deltaTime;
        }

        
    }

   void ChaseTarget()
    {
        Vector3 direction =(_target.position - transform.position).normalized;

        if(_controller.isGrounded == false)
        {
            _verticalVelocity -= _gravity * Time.deltaTime;
           
        }

        direction.y =_verticalVelocity;

        _controller.Move(direction * Time.deltaTime * _speed);
    }

    void FaceTarget()
    {
        Vector3 direction = (_target.position - transform.position).normalized;

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


