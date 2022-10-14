using Unity.VisualScripting;
using UnityEngine;
public enum ImpairedType { Minus, Divide }
[CreateAssetMenu(fileName = "Effect", menuName = "Effect/Effect", order = 1)]
public class Effect : ScriptableObject
{
    public string _effectName;
    public int _amount;
    public int _duration;
    public bool isMovementAffected;
    public ImpairedType _type;
    
}
