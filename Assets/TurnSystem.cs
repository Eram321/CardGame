using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour {

    [Header("Parametrs")]
    [SerializeField] float roundStartDelay;
    [SerializeField] float turnStartDelay;
    [SerializeField] float turnTime;

    [Header("Heroes")]
    [SerializeField] PlayerController[] players;
    public PlayerController currentPlayer;
    public bool IsHeroTurn(int heroID)
    {
        return false;
    }

    public bool turnDone;
    public bool PreRound = true;
        
    void Awake()
    {
        if (GameManager.TurnSystem == null)
            GameManager.TurnSystem = this;
        else
            Destroy(this.gameObject);
    }
        
    // Use this for initialization
	void Start () {
        StartCoroutine(MainGameLoop()); // Start battle
	}

    private IEnumerator MainGameLoop()
    {
        while (true) {

            //Noise battle start
            yield return new WaitForSeconds(0.1f);

            //Disable all players at start
            foreach (var p in players) { p.IsPlayerTurn = false; }
            
            //Start Round
            Debug.Log("Round Started");
            yield return new WaitForSeconds(roundStartDelay);

            //Select Player
            SelectPlayer();
        
            //Start Turn for first player
            yield return StartCoroutine(TurnStart()); //if corunite return turn is end

            //End turn
            TurnEnd();

            //Battle phase
            yield return StartCoroutine(BattlePhase());

            //repeat
        }
    }

    private void SelectPlayer()
    {
        //Select player
        if (currentPlayer == players[1] || currentPlayer == null) currentPlayer = players[0];
        else if (currentPlayer == players[0]) currentPlayer = players[1];

        //Enable player
        currentPlayer.IsPlayerTurn = true;
    }

    public bool endTurn;
    public void EndTurn() { GameManager.TurnSystem.endTurn = true; }
    private IEnumerator TurnStart()
    {
        //Take card from deck
        yield return StartCoroutine(currentPlayer.StartTurn());

        Debug.Log(currentPlayer + " :TURN");
        endTurn = false;

        var t = 0f;
        while (!endTurn) { 
            GameManager.GameUI.SetTurnTime(turnTime - t);
            yield return new WaitForSeconds(1f);
            t += 1f;
            if (t > turnTime) endTurn = true;
        }
    }


    public IEnumerator BattlePhase()
    {
        // zaleznie od tego jakim jestesmy playererm
        // przesun wszystkie jednoski o speed pól do przodu
        var lines = GameManager.MapController.GetLines();

        //Loop all lines
        foreach (var line in lines)
        {
           
            //Depends on player
            if(currentPlayer == players[0])
            {
                yield return StartCoroutine(FirstPlayerTurn(line));   
            }
            else if (currentPlayer == players[1])
            {
                yield return StartCoroutine(SecondPlayerTurn(line));
            }
            
        }

        yield return new WaitForSeconds(1f);
    }


    public IEnumerator FirstPlayerTurn(Line line)
    {
        //and all places in line
        var places = line.GetPlaces();
        
        //Get units
        var units = players[0].GetUnits();
        var enemyUnits = players[1].GetUnits();

        for (int i = places.Count-1; i >= 0; i--)
        {
            //check if is unit on place
            if (places[i].unit)
            {
                //check if unit is current player unit and make action
                if (units.Contains(places[i].unit))
                {
                    //Get unit object
                    var u = places[i].unit;

                    //Movement range
                    var moveRange = 0;
                    var maxMoveRange = (places.Count - 1 - i);
                    if (u.Card.Speed > maxMoveRange) moveRange = maxMoveRange; else moveRange = u.Card.Speed;
                    var currentPos = i;

                    var actionDone = false;
                    while (currentPos < i + moveRange)
                    {
                        //Is enemy in range
                        var attackRange = 0;
                        var maxAttackRange = (places.Count - 1 - currentPos);
                        if (u.Card.Range > maxAttackRange) attackRange = maxAttackRange; else attackRange = u.Card.Range;
                        for (int x = currentPos + 1; x <= currentPos + attackRange; x++)
                        {
                            if (places[x].unit)
                            {
                                if (!units.Contains(places[x].unit))
                                {
                                    //Play attack anim
                                    yield return StartCoroutine(u.PlayAttackAnimation(false));
                                    
                                    //Damage enemy unit if anim completed
                                    var destroyed = places[x].unit.CalculateDamage(places[currentPos].unit);
                                    if (destroyed)
                                    {
                                        enemyUnits.Remove(places[x].unit);
                                        Destroy(places[x].unit.gameObject);
                                        places[x].unit = null;
                                    }
                                    actionDone = true;
                                    break;
                                }
                            }
                        }
                        if (actionDone) break;

                        //Attack commander?
                        if(u.Card.Range+1 > maxAttackRange)
                        {
                            //Play attack anim
                            yield return StartCoroutine(u.PlayAttackAnimation(false));
                            
                            //Attack commander
                            players[1].AttackCommander(u.Card.Attack);
                            break;
                        }

                        //If place is empty Move 
                        if (!(places[currentPos + 1].unit) && currentPos+1 != places.Count-1)
                        {
                            //Move one place forward
                            yield return StartCoroutine(places[currentPos + 1].MoveUnit(u, false));
                            //Clear current Place
                            places[currentPos].unit = null;
                            currentPos++;
                        }
                        else // if way is blocked break
                            break;

                    }
                    i = currentPos;
                }
            }
        }
    }

    public IEnumerator SecondPlayerTurn(Line line)
    {
        //and all places in line
        var places = line.GetPlaces();

        //Get units
        var units = players[1].GetUnits();
        var enemyUnits = players[0].GetUnits();

        for (int i = 1; i < places.Count; i++)
        {
            //check if is unit on place
            if (places[i].unit)
            {
                //check if unit is current player unit and make action
                if (units.Contains(places[i].unit))
                {
                    //Get unit object
                    var u = places[i].unit;

                    //Movement range
                    var moveRange = 0;
                    var maxMoveRange = i;
                    if (u.Card.Speed > maxMoveRange) moveRange = maxMoveRange; else moveRange = u.Card.Speed;
                    var currentPos = i;

                    var actionDone = false;
                    while (currentPos > i-moveRange)
                    {
                        //Is enemy in range
                        var attackRange = 0;
                        var maxAttackRange = i;
                        if (u.Card.Range > maxAttackRange) attackRange = maxAttackRange; else attackRange = u.Card.Range;
                        for (int x = currentPos - 1; x >= currentPos-attackRange; x--)
                        {
                            if (places[x].unit)
                            {
                                if (!units.Contains(places[x].unit))
                                {
                                    //Play attack anim
                                    yield return StartCoroutine(u.PlayAttackAnimation(true));

                                    //Damage enemy unit if anim completed
                                    var destroyed = places[x].unit.CalculateDamage(places[currentPos].unit);
                                    if (destroyed)
                                    {
                                        enemyUnits.Remove(places[x].unit);
                                        Destroy(places[x].unit.gameObject);
                                        places[x].unit = null;
                                    }

                                    actionDone = true;
                                    break;
                                }
                            }
                        }
                        if (actionDone) break;

                        //Attack commander?
                        if (u.Card.Range+1 > maxAttackRange)
                        {
                            //Play attack anim
                            yield return StartCoroutine(u.PlayAttackAnimation(true));

                            //Attack commander
                            players[0].AttackCommander(u.Card.Attack);
                            break;
                        }

                        //If place is empty Move 
                        if (!(places[currentPos - 1].unit) && currentPos-1 != 0)
                        {
                            //Move one place forward
                            yield return StartCoroutine(places[currentPos - 1].MoveUnit(u, true));

                            //Clear current Place
                            places[currentPos].unit = null;
                            currentPos--;
                        }
                        else // if way is blocked break
                            break;

                    }

                    i = currentPos;
                }
            }
        }
    }

    public void TurnEnd()
    {
        Debug.Log("turnEnd");
        currentPlayer.DiselectCard();
        currentPlayer.IsPlayerTurn = false;
        GameManager.GameUI.ResetUI();
    }

    internal PlayerController GetPlayer(int v)
    {
        return players[v];
    }

}
