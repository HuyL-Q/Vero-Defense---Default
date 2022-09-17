using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NguyenVanLoc : HeroTower
{
    public override void Start()
    {
        if(!flag)
        StartCoroutine(SetTower("tower_hero_5"));
        Animator = GetComponentInChildren<Animator>();
        base.Start();
    }
}
