using System;
using UnityEngine;

public class Burn : ADebuff
{
    float damage;
    float timer;

    public float Damage { get => damage; set => damage = value; }

    // Start is called before the first frame update
    void Awake()
    {
        Duration = 10;
        Damage = 1;
        timer = 0.1f;
        gameObject.AddComponent<ParticleSystem>();
    }
    public override void Update()
    {
        base.Update();
        if (this.enabled)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                gameObject.GetComponent<AEnemy>().ReceiveDamage(Damage);
                timer = 0.1f;
            }
        }
    }
    public override void OnEnable()
    {
        var className = this.GetType().Name;
        base.OnEnable();
    }
    public override void OnDisable()
    {
        base.OnDisable();
    }
}
