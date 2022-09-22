using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LyVanBuu : HeroTower
{
    public override void Skill()
    {
        throw new System.NotImplementedException();
    }

    public override void Start()
    {
        if(!flag)
        StartCoroutine(SetTower("tower_hero_4"));
        Animator = GetComponentInChildren<Animator>();
        base.Start();
    }   
}
