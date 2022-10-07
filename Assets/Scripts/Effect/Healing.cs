using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : ABuff
{
    float amount;
    float timer;
    public float Amount { get => amount; set => amount = value; }
    private void Awake()
    {
        Effected.Add(gameObject.GetComponent<AEnemy>());
        Duration = 10;
        amount = 1;
        timer = 0.1f;
    }
    public override void Affect(AEnemy enemy)
    {
        enemy.HP += amount;
    }
    public override void Update()
    {
        base.Update();
        if (enabled)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                foreach (var effect in Effected)
                {
                    Affect(effect);
                }
                timer = 0.1f;
            }
        }
    }
}
