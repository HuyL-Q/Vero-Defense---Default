using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranQuangDieu : HeroTower
{
    public override void Skill()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    public override void Start()
    {
        if(!flag)
        StartCoroutine(SetTower("tower_hero_7"));
        Animator = GetComponentInChildren<Animator>();
        base.Start();
    }
}
