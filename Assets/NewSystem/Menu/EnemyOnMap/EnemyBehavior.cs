using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBehaviour", menuName = "Enemy/Behaviour", order = 1)]
public class EnemyBehavior : ScriptableObject {
    public List<Vector3> Positions = new List<Vector3>();
}
