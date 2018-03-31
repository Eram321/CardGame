﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core { 
    public class TurnSystem : MonoBehaviour {

        private static TurnSystem instance;
        public static TurnSystem Instance
        {
            get {
                return instance;
            }
        }

        [Header("Parametrs")]
        [SerializeField] float battleStartDelay;
        [SerializeField] float turnStartDelay;
        [SerializeField] float turnTime;


        [Header("Heroes")]
        [SerializeField] Hero[] heroes;
        Hero currentHero;
        public int CURRENT_HERO_ID
        {
            get {
                return currentHero ? currentHero.HERO_ID : 0;
            }
        }

        int iter;
	    
        void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this.gameObject);

            DontDestroyOnLoad(this);
        }
        
        // Use this for initialization
	    void Start () {

            Hero.onHeroEndTurn += TurnEnd;

            StartCoroutine(BattleStart()); // Start battle
	    }

        private IEnumerator BattleStart()
        {
            Debug.Log("battkeStart");

            //kto kontra kto

            yield return new WaitForSeconds(battleStartDelay);

            currentHero = heroes[0]; // random hero

            StartCoroutine(TurnStart());
        }

        private IEnumerator TurnStart()
        {
            //Take card from deck

            yield return new WaitForSeconds(turnStartDelay);

            //enable input for player
            currentHero.StartNewTurn();

            Debug.Log("nextTurn");
            yield return new WaitForSeconds(turnTime);

           // TurnEnd();
        }

        public void TurnEnd()
        {
            Debug.Log("turnEnd");

            //Handle battle here

            //Chose next hero
            if (iter == heroes.Length - 1) iter = 0; else iter++;
            currentHero = heroes[iter];

            //Start next turn
            StartCoroutine(TurnStart());
        }

        void OnDestroy()
        {
            Hero.onHeroEndTurn -= TurnEnd;
        }
    }
}