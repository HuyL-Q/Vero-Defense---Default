using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NguyenVanTuyet : AMageHero
{
    bool firstTimeStartBuff;
    [SerializeField]
    List<GameObject> towers;
    private float _buffAmount;
    public List<GameObject> Towers { get => towers; set => towers = value; }
    public override void Start()
    {
        _buffAmount = 10f;
        firstTimeStartBuff = true;
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
            towerScript.Damage += (int)_buffAmount;
            towerScript.AttackSpeed -= _buffAmount/100f;
        }
        firstTimeStartBuff = false;
    }

    public override void ClearBuff()
    {
        throw new System.NotImplementedException();
    }

    private void OnDestroy()
    {
        ClearBuff();
    }

    public override IEnumerator Skill()
    {
        throw new System.NotImplementedException();
    }
}
