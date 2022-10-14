using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : ADebuff
{
    float baseSpeed;
    float slowPercent;

    public float BaseSpeed { get => baseSpeed; set => baseSpeed = value; }
    public float SlowPercent { get => slowPercent; set => slowPercent = value; }

    // Start is called before the first frame update
    void Awake()
    {
        Duration = 10;
        //^ setData
        base.Start();
        slowPercent = 0.5f;
        BaseSpeed = gameObject.GetComponent<AEnemy>().Agent.speed;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if(this.enabled)
        {
            gameObject.GetComponent<AEnemy>().Agent.speed = slowPercent * BaseSpeed;
        }
        else
        {
            BaseSpeed = gameObject.GetComponent<AEnemy>().Agent.speed;
        }
    }
    public override void OnDisable()
    {
        gameObject.GetComponent<AEnemy>().Agent.speed = BaseSpeed;
        base.OnDisable();
    }

}
