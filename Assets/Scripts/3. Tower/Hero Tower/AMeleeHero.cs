using UnityEngine;

public abstract class AMeleeHero : HeroTower
{
    public float timer;
    public override void Attack()
    {
        foreach (GameObject enemy in Monsters)
        {
            var script = enemy.GetComponent<AEnemy>();
            script.ReceiveDamage(Damage);
        }
        //Animator.SetBool("IsAttack", false);
    }

    public override void Update()
    {
        timer -= Time.deltaTime;
        if (Monsters.Count > 0)
        {
            if (SkillReady && !SkillActivated)
            {
                //Debug.Log("Start skill.");
                SkillActivated = true;
                StartCoroutine(Skill());
            }
            Animator.SetBool("IsAttack", true);
            Vector2 direction = Monsters[0].transform.position - transform.position;
            Animator.SetFloat("Horizontal", direction.x);
            Animator.SetFloat("Vertical", direction.y);
            //Attack();
        }
        else
        {
            Animator.SetBool("IsAttack", false);
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
