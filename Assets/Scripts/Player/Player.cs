using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{

    [SerializeField]
    int _maxHealth;

    public static Action<int> OnHealthChange;


    public int Health{get; set;}

    void Start()
    {
        Health = _maxHealth;
        OnHealthChange(Health);
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
