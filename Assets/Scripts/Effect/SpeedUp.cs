using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : ABuff
{
    float timer;
    float amount;
    public override void Affect(AEnemy enemy)
    {
        enemy.Agent.speed = enemy.Agent.speed + amount;
    }

    private void Awake()
    {
        Effected.Add(gameObject.GetComponent<AEnemy>());
        Duration = 10;
        amount = 0.02f;
        timer = 0.1f;
    }

    // Update is called once per frame
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
