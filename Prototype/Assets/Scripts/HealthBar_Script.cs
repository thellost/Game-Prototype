using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar_Script : MonoBehaviour
{
    private image HealthBar;
    public float Currenthealth;
    private float MaxHealth = 100f;
    playescript Player;

    private void Start()
    {
        HealthBar = GetComponent<image>();
        Player = FindObjectOfType<skrippleyer>();
    }

    private void Upadate()
    {
        Currenthealth = Player.Health;
        HealthBar.fillmount = Currenthealth / MaxHealth;
    }
}
