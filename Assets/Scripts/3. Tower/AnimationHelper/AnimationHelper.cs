using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper : MonoBehaviour
{
    ATower _tower;
    // Start is called before the first frame update
    void Start()
    {
        _tower = GetComponentInParent<ATower>();
    }

    void Attack()
    {
        _tower.Attack();
    }
}
