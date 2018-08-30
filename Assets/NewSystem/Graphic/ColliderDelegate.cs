using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDelegate : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDelegateReciver reciver = (IDelegateReciver)other.GetComponent(typeof(IDelegateReciver));
        if (reciver != null)
            reciver.OnRecive();
    }
}
