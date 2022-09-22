using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuangTrung : HeroTower
{
    public override void Start()
    {
        if(!flag)
        StartCoroutine(SetTower("tower_hero_1"));
        Animator = GetComponentInChildren<Animator>();
        base.Start();
    }
    
}
