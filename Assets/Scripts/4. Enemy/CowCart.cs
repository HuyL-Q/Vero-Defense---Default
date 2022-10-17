using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowCart : AEnemy
{
    // Update is called once per frame
    public override void Start()
    {
        base.Start();
    }
    public override void OnEnable()
    {
        base.OnEnable();
        AEffect.Active(gameObject, new List<System.Type> { typeof(Cleanse) }, 100);
    }
}
