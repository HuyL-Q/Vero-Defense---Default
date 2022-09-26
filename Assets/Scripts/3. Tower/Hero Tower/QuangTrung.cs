using UnityEngine;

public class QuangTrung : AMeleeHero
{
    public override void Skill()
    {
        throw new System.NotImplementedException();
    }

    public override void Start()
    {
        if (!flag)
            StartCoroutine(SetTower("tower_hero_1"));
        Animator = GetComponentInChildren<Animator>();
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }
}
