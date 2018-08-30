using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourRecorder : MonoBehaviour {

    [SerializeField] EnemyBehavior EnemyBehavior;
    [SerializeField] float TimeBetweenRecords;

    internal void StartRecording()
    {
        StartCoroutine(StartRecord());
    }

    private IEnumerator StartRecord()
    {
        EnemyBehavior.Positions.Clear();
        while (true)
        {
            EnemyBehavior.Positions.Add(transform.position);
            yield return new WaitForSeconds(TimeBetweenRecords);
        }
    }

    internal void StopRecoring()
    {
        StopAllCoroutines();
    }
}
