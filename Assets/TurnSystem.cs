using System;
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
        [SerializeField] HeroController[] heroes;
        public HeroController currentHero;
        public bool IsHeroTurn(int heroID)
        {
            if(currentHero)
                if (currentHero.HERO_ID == heroID)
                    return true;

            return false;
        }

        public bool turnDone;
       
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
            currentHero.IsHeroActionEnabled = true;

            Debug.Log("nextTurn");
            yield return new WaitForSeconds(turnTime);

           // TurnEnd();
        }
        public IEnumerator BattlePhase()
        {
            currentHero.IsHeroActionEnabled = false;
            yield return new WaitForSeconds(0.5f);

            var tmp = iter;
            if (tmp == heroes.Length - 1) tmp = 0; else tmp++;
            var opponentAreas = heroes[tmp].GetDroppableAreas();
            var areas = currentHero.GetDroppableAreas();

            var battleWaitTime = 2f;
            //First Line 4 card slots
            //And second line 4 slots
            for (int j = 0; j < 2; j++) { 
                for (int i = 0; i < 4; i++)
                {
                    var slot = areas[j*4 + i];
                    Transform cardObj = null;

                    if (slot.transform.childCount > 0)
                        cardObj = slot.transform.GetChild(0); // get first child (is a card)
                    
                    //if slot is not empty
                    if (cardObj)
                    {
                        //for each card in enemy line
                        for (int e = 0; e < 2; e++)
                        {
                            var enemySlot = opponentAreas[e*4 + i];
                            Transform enemyobj = null;

                            if (enemySlot.transform.childCount > 0)
                                enemyobj = enemySlot.transform.GetChild(0);

                            if (enemyobj)
                            {
                                var card = cardObj.GetComponent<DraggableCard>();
                                var enemyCard = enemyobj.GetComponent<DraggableCard>();

                                StartCoroutine(card.StartAttack(enemyCard));
                                break;
                            }

                            //if attack not blocked attack hero
                            if(e == 1) {
                                var card = cardObj.GetComponent<DraggableCard>();
                                StartCoroutine(card.HeroAttack(heroes[tmp]));
                            }

                            battleWaitTime = 0.1f;
                        }
                    }
                }

                yield return new WaitForSeconds(battleWaitTime);
            }


            //foreach (var area in areas)
            //{
            //    for (int i = 0; i < area.transform.childCount; i++)
            //    {
            //        var child = area.transform.GetChild(i);
                    
            //        foreach (var opponentArea in opponentAreas)
            //        {
            //            Transform opponentChild = null;
            //            if (i < opponentArea.transform.childCount)
            //                opponentChild = opponentArea.transform.GetChild(i);
                        
            //            if (opponentChild)
            //            {
            //                var card = child.GetComponent<DraggableCard>();
            //                var opponentCard = opponentChild.GetComponent<DraggableCard>();

            //                StartCoroutine(card.StartAttack(opponentCard));
            //                break;
            //            }
            //        }
           
            //    }

            //    //second line
            //    yield return new WaitForSeconds(2f);
            //}

            //Reset Card UI
            currentHero.ResetUI();

            currentHero = null;

            yield return new WaitForSeconds(2);

            //Chose next hero
            if (iter == heroes.Length - 1) iter = 0; else iter++;
            currentHero = heroes[iter];

            //Start next turn
            StartCoroutine(TurnStart());

        }

        public void TurnEnd()
        {
            Debug.Log("turnEnd");

            //Handle battle here
            StartCoroutine(BattlePhase());
        }

        void OnDestroy()
        {
            Hero.onHeroEndTurn -= TurnEnd;
        }
    }
}
