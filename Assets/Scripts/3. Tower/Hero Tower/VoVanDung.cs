using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoVanDung : HeroTower
{
    public override void Start()
    {
        if(!flag)
        StartCoroutine(SetTower("tower_hero_8"));
        Animator = GetComponentInChildren<Animator>();
        base.Start();
    }
}