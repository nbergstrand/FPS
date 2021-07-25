using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    Enemy _enemy;


    private void Start()
    {
        _enemy = GetComponentInParent<Enemy>();
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _enemy.SetState(EnemyState.Attack);
        }


    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            
            _enemy.SetState(EnemyState.Chase);
        }
        
    }

}
