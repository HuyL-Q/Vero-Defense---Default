using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeVanHung : AMeleeHero
{
    public override void Skill()
    {
        throw new System.NotImplementedException();
    }

    public override void Start()
    {
        if(!flag)
        StartCoroutine(SetTower("tower_hero_3"));
        Animator = GetComponentInChildren<Animator>();
        base.Start();
    }
    
}
