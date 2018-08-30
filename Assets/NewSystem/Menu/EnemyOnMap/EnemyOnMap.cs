using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnMap : MonoBehaviour {

    [SerializeField] EnemyBehavior behaviour;
    [SerializeField] float moveSpeed = 2.5f;
    [SerializeField] float waitOnPosTime = 1f;

    private void Start()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (true)
        {
            foreach (var pos in behaviour.Positions){
                yield return StartCoroutine(MoveTo(pos));
            }
        }
    }

    private IEnumerator MoveTo(Vector3 pos)
    {
        var dis = Vector3.Distance(transform.position, pos);
        while (dis > 0.1f)
        {
            dis = Vector3.Distance(transform.position, pos);
            transform.position = Vector3.MoveTowards(transform.position, pos, moveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
}
