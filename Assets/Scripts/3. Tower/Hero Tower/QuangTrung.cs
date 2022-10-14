using System.Collections;
using UnityEngine;

public class QuangTrung : AMeleeHero
{
    [SerializeField]
    GameObject _skillEffect;
    ParticleSystem _particleSystem;
    GameObject fx;
    public override IEnumerator Skill()
    {
        if (fx == null)
        {
            fx = Instantiate(_skillEffect, Monsters[0].transform.position + new Vector3(5, 5), Quaternion.identity);
        }
        else
        {
            fx.SetActive(true);
        }
        if (!_particleSystem.isPlaying)
        {
            _particleSystem.Play();
        }
        while (SkillActivated)
        {
            foreach (GameObject enemy in Monsters)
            {
                enemy.GetComponent<AEnemy>().ReceiveDamage(Damage * 2);
            }
            SkillGauge -= 10;
            yield return new WaitForSeconds(1f);
            if (SkillGauge <= 0)
            {
                SkillGauge = 0;
                SkillActivated = false;
                SkillReady = false;
                //_particleSystem.Stop();
                //Destroy(fx.gameObject);
                yield break;
            }
        }
    }

    public override void Start()
    {
        _particleSystem = _skillEffect.GetComponent<ParticleSystem>();
        _particleSystem.Stop();
        var main = _particleSystem.main;
        main.duration = 9.5f;
        timer = 1;
        base.Start();
    }
}
