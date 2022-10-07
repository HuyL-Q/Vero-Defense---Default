using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulnerable : ADebuff
{
    float additionDamage;

    public float AdditionDamage { get => additionDamage;private set => additionDamage = value; }

    // Start is called before the first frame update
    void Awake()
    {
        AdditionDamage = 20;
        Duration = 10;
    }
    public void SetAdditionDamage(ref float baseDamage)
    {
        AdditionDamage = baseDamage * 1/2;
        baseDamage += AdditionDamage;
    }
}
