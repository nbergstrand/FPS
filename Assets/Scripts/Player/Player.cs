using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{

    [SerializeField]
    int _maxHealth;

    public int Health { get; set; }

    public static Action<int> OnHealthChange;
    
    void Start()
    {
        Health = _maxHealth;

        if (OnHealthChange != null)
        {
            OnHealthChange(Health);
        }
    }


    public void Damage(int damageAmount)
    {
        Health -= damageAmount;
        if(OnHealthChange != null)
        {
            OnHealthChange(Health);
        }
    }

}
