using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ARangeHero : HeroTower
{
    public float timer;
    public override void Update()
    {
        base.Update();
        timer -= Time.deltaTime;
        if (Monsters.Count > 0)
        {
            if (SkillReady && !SkillActivated)
            {
                //Debug.Log("Start skill.");
                SkillActivated = true;
                StartCoroutine(Skill());
            }
            //Animator.SetBool("IsAttack", true);
            //Vector2 direction = Monsters[0].transform.position - transform.position;
            //Animator.SetFloat("Horizontal", direction.x);
            //Animator.SetFloat("Vertical", direction.y);
            //Attack();
        }
        else
        {
            //Animator.SetBool("IsAttack", false);
            StopCoroutine(Skill());
        }
        if (!SkillReady && timer <= 0)
        {
            timer = .1f;
            SkillGauge += 1;
        }
        if (SkillGauge >= 100)
        {
            SkillGauge = 100;
            SkillReady = true;
        }
    }
}
