using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NguyenVanTuyet : HeroTower
{
    public override void Start()
    {
        if(!flag)
        StartCoroutine(SetTower("tower_hero_6"));
        Animator = GetComponentInChildren<Animator>();
        base.Start();
    }
}