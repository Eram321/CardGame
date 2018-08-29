using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Commander : MonoBehaviour {

   // [SerializeField] Text healthText;
    [SerializeField] Image healthFill;

    public float Health
    {
        get { return health; }
        set {
            health = value;

            if (health <= 0)
                GameManager.TurnSystem.GameOver();

            healthFill.fillAmount = health / maxHealth;
        }
    }

    [SerializeField] float maxHealth;
    float health;

    private void Start()
    {
        Health = maxHealth;
    }
}
