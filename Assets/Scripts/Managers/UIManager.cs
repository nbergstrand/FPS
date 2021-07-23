using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    Text _playerHealth;

    private void Start()
    {
        Player.OnHealthChange += UpdatePlayerHealthUI;
    }

    void UpdatePlayerHealthUI(int health)
    {
        _playerHealth.text = "Health: " + health;
    }

}
