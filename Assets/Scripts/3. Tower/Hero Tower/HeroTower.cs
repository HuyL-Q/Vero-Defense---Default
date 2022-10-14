using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class HeroTower : ATower
{
    private bool _skillReady;
    private bool _skillActivated;
    [SerializeField]
    private int _skillGauge;
    
    Animator _animator;

    public Animator Animator { get => _animator; set => _animator = value; }

    public override double AttackSpeed
    {
        get { return base.AttackSpeed; }
        set
        {
            base.AttackSpeed = value;
            Animator.speed = (float)(1 / base.AttackSpeed);
        }
    }
    public bool SkillReady { get => _skillReady; set => _skillReady = value; }
    public bool SkillActivated { get => _skillActivated; set => _skillActivated = value; }
    public int SkillGauge { get => _skillGauge; set => _skillGauge = value; }

    public override void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        base.Awake();
    }
    public override void Start()
    {
        base.Start();
        SkillReady = false;
        SkillActivated = false;
        SkillGauge = 0;
        if (GameController.instance.HeroList.GetValueOrDefault(int.Parse(ID.Split("_")[2])))
            SellButton(PlacementIndex);
        GameController.instance.HeroList[int.Parse(this.ID.Split("_")[2])] = true;
    }

    public void SellButton(int placementIndex)
    {
        TowerManager.instance.TowerPlacementParent.transform.GetChild(placementIndex).gameObject.SetActive(true);
        Destroy(gameObject);
        GameController.instance.PlayerMoney += this.Price;
        StoryUIController.instance.UpdateGoldIndex();
    }
    public override void Update(){
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

    public abstract IEnumerator Skill();
}
