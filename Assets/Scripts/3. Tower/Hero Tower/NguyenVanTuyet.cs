using System.Collections.Generic;
using UnityEngine;

public class NguyenVanTuyet : AMageHero
{
    bool firstTimeStartBuff;
    [SerializeField]
    List<GameObject> towers;
    public List<GameObject> Towers { get => towers; set => towers = value; }
    public override void Start()
    {
        firstTimeStartBuff = true;
        if (!flag)
            StartCoroutine(SetTower("tower_hero_6"));
        Animator = GetComponentInChildren<Animator>();
        base.Start();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            if (!firstTimeStartBuff)
            {
                ClearBuff();
            }
            towers.Add(collision.transform.parent.gameObject);
            Buff();
        }
        base.OnTriggerEnter2D(collision);
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            towers.Remove(collision.transform.parent.gameObject);
        }
        base.OnTriggerExit2D(collision);
    }

    public override void Buff()
    {
        //throw new System.NotImplementedException();
        foreach (GameObject tower in towers)
        {
            ATower towerScript = tower.GetComponent<ATower>();
            towerScript.Damage += 10;
            towerScript.AttackSpeed -= 0.3f;
        }
        firstTimeStartBuff = false;
    }

    public override void ClearBuff()
    {
        //throw new System.NotImplementedException();
        foreach (GameObject tower in towers)
        {
            ATower towerScript = tower.GetComponent<ATower>();
            towerScript.Damage -= 10;
            towerScript.AttackSpeed += 0.3f;
        }
    }

    public override void Debuff()
    {
        throw new System.NotImplementedException();
    }
}
