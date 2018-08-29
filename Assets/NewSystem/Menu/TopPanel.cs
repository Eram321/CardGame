using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopPanel : MonoBehaviour {

    public void OpenTroopsWindow()
    {
        Menu.Instance.OpenWindow(typeof(CardsPanel));
    }
	
}
