using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
       
    [SerializeField]
    EnemyState _state;

    Transform _targetTransform;
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

    Animator _animator;

    bool _isDead = false;

    Vector3 velocity;
    Vector3 previousPosition;


    void Start()
    {
        Health = _maxHealth;

        _targetTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _target = _targetTransform.GetComponent<IDamageable>();

        _controller = GetComponent<CharacterController>();

        _animator = GetComponent<Animator>();

        if (_targetTransform == null)
        {
            Debug.LogError("No player found");
        }
    }

    void Update()
    {


       if (_state == EnemyState.Chase)
       {
            ChaseTarget();
            _animator.SetBool("chasing", true);
            
       }

       if(_state == EnemyState.Chase || _state == EnemyState.Attack)
        {
            FaceTarget();
        }


        if (_state == EnemyState.Attack)
        {
            AttackPlayer();
            _animator.SetBool("chasing", false);

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
            _animator.SetTrigger("punch");
            _attackCooldownTimer = _attackCooldown;
            _target.Damage(10);

        }

        _attackCooldownTimer -= Time.deltaTime;
    }

   void ChaseTarget()
    {
        Vector3 direction =(_targetTransform.position - transform.position).normalized;

        if(_controller.isGrounded == false)
        {
            _verticalVelocity -= _gravity * Time.deltaTime;
           
        }

        direction.y =_verticalVelocity;

       

        _controller.Move(direction * Time.deltaTime * _speed);
    }

    void FaceTarget()
    {
        Vector3 direction = (_targetTransform.position - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);

    }

    

    public void Damage(int damageAmount)
    {
        Health -= damageAmount;
        _animator.SetTrigger("hit");
        _state = EnemyState.Hit;
        StartCoroutine(Hit());

        if(Health <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        _state = EnemyState.Dead;
        _animator.SetBool("dead", true);
        GetComponentInChildren<SphereCollider>().enabled = false;
    }

    IEnumerator Hit()
    {
        yield return new WaitForSeconds(1.5f);
        _state = EnemyState.Chase;
        if (Health <= 0)
        {
            Die();
        }
    }

}

public enum EnemyState
{
    Idle,
    Chase,
    Attack,
    Hit,
    Dead
}

