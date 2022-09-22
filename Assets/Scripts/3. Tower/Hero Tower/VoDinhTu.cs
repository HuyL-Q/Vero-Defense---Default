using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoDinhTu : HeroTower
{
    public override void Start()
    {
        if(!flag)
        StartCoroutine(SetTower("tower_hero_2"));
        Animator = GetComponentInChildren<Animator>();
        base.Start();
    }
    
}
