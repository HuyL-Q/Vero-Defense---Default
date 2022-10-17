using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Tower", menuName = "ScriptableObjects/Tower Scriptable Object", order = 2)]
public class TowerScriptableObject : ScriptableObject
{
    public string _id;
    public int _attack;
    public float _attackSpeed;
    public float _range;
    public int _price;
    public List<string> _special;
}
