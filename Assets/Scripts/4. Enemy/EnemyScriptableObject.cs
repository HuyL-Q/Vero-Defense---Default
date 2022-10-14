using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Enemy Scriptable Object", order = 1)]
public class EnemyScriptableObject : ScriptableObject
{
    public string id;
    public int hp;
    public float speed;
    public int damage;
    public int killReward;
    public List<string> Effect;
}
