using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HeroTower : ATower
{
    public bool flag = false;

    public class TowerConverter : JsonConverter<List<TowerJs>> { }

    Animator _animator;

    public Animator Animator { get => _animator; set => _animator = value; }

    public override int GetSize()
    {
        return Size;
    }
    public override void Start()
    {
        if (GameController.instance.HeroList.GetValueOrDefault(int.Parse(ID.Split("_")[2])))
            SellButton(PlacementIndex);
        base.Start();
        GameController.instance.HeroList[int.Parse(ID.Split("_")[2])] = true;
    }
    public override IEnumerator SetTower(string id)
    {
        //PriceToUpgrade.Clear();
        //import data from json here
        //string[] idSplit = id.Split("_");
        //string nextID = idSplit[0] + "_" + idSplit[1] + "_" + (int.Parse(idSplit[2]) + 1);
        TowerConverter tc = new TowerConverter();
        tc.setCurrentDir(@"\TowerStat.json");
        List<TowerJs> towerList = tc.getObjectFromJSON();
        foreach (TowerJs tower in towerList)
        {
            if (tower.id == id)
            {
                ID = tower.id;
                Damage = tower.attack;
                Range = tower.range;
                AttackSpeed = tower.attackSpeed;
                Price = tower.Cost;
                Size = tower.height;
                //Data = true;
            }
        }
        flag = true;
        transform.GetChild(2).localScale = new Vector2(Range, Range);
        gameObject.name = ID;
        yield return null;
    }
    public void SellButton(int placementIndex)
    {
        TowerManager.instance.TowerPlacementParent.transform.GetChild(placementIndex).gameObject.SetActive(true);
        Destroy(gameObject);
        GameController.instance.PlayerMoney += Price;
        StoryUIController.instance.UpdateGoldIndex();
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
