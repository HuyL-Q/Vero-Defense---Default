using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ArcherTower : ATower
{
    Animator _animator;
    public Animator Animator { get => _animator; set => _animator = value; }
    public override void Start()
    {
        Animator = GetComponentInChildren<Animator>();
        base.Start();
    }

    public override void Update()
    {
        ShootTimer -= Time.deltaTime;
        if (ShootTimer <= 0f)
        {

            ShootTimer = AttackSpeed;
            UpdateEnemy();
            if (CurrentEnemy != null)
            {
                Animator.SetBool("IsAttack", true);
                Vector2 direction = CurrentEnemy.transform.position - transform.position;
                Animator.SetFloat("Horizontal", direction.x);
                Animator.SetFloat("Vertical", direction.y);
                ShootPosition = transform.GetChild(3).position;
                Attack();
            }
            else
            {
                Animator.SetBool("IsAttack", false);
                transform.rotation = Quaternion.identity;
                //transform.GetChild(3).rotation = Quaternion.identity;
            }
        }
    }
}