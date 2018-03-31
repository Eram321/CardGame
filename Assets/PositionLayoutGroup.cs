using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class PositionLayoutGroup : MonoBehaviour {


    void Update()
    {
        if (transform.childCount == 0) return;

        var childCout = transform.childCount;

        float offset = 100;
        if (childCout >= 10)
            offset = 1000 / childCout;

        var lastChild = transform.GetChild(childCout - 1);
        var pos = (childCout - 1) * offset + offset/2f;
        lastChild.position = new Vector2(pos, 50);

        for (int i = 1; i < childCout; i++)
        {
            var child = transform.GetChild(i);
            var position = i * offset + offset/2f;
            child.position = new Vector2(position, 50);
        }
    }
}
