using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranQuangDieu : HeroTower
{
    // Start is called before the first frame update
    public override void Start()
    {
        if(!flag)
        StartCoroutine(SetTower("tower_hero_7"));
        Animator = GetComponentInChildren<Animator>();
        base.Start();
    }
}
