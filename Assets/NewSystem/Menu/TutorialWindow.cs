using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialWindow : Window {

    public override void ButtonClick()
    {
        base.ButtonClick();
        Game.Instance.CurrentEnemyDeck = 1;
        Game.Instance.States.TutorialCompleted = true;
        SceneManager.LoadScene("BattleMap");
    }


}
