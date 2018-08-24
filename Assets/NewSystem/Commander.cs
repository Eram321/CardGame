using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Commander : MonoBehaviour {

    [SerializeField] Text healthText;

    public float Health
    {
        get { return health; }
        set {
            health = value;
            healthText.text = health.ToString(); ;

            if (health == 0) ;//die
        }
    }
    [SerializeField] float health;

    private void Start()
    {
        Health = health;
    }
}
