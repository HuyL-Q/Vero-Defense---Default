using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LyVanBuu : ARangeHero
{
    public GameObject ArrowSprite;
    public override IEnumerator Skill()
    {
        if (SkillActivated)
        {
            for(int i = 0; i < 3; i++)
            {
                GameObject arrow = Instantiate(ArrowSprite, gameObject.transform.position, Quaternion.identity);
                arrow.GetComponent<SkillArrow>().Damage = Damage * 1.5f;
                yield return new WaitForSeconds(1f);
            }
            //SkillGauge -= 10;
            //if (SkillGauge <= 0)
            //{
            //    SkillGauge = 0;
            //    SkillActivated = false;
            //    SkillReady = false;
            //    //_particleSystem.Stop();
            //    //Destroy(fx.gameObject);
            //    yield break;
            //}
            SkillActivated = false;
        }
    }

    public override void Start()
    {
        timer = 1;
        base.Start();
    }
}
