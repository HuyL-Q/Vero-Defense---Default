using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class ATower : MonoBehaviour, ITower
{
    private int placementIndex;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    List<GameObject> monsters;
    [SerializeField]
    GameObject rangeIndicator;
    private string id;
    private int damage;
    private float range;
    [SerializeField]
    private double attackSpeed;
    private int price;
    private int size;
    private List<string> special;
    private Vector3 shootPosition;
    private GameObject currentEnemy;
    public ObjectPool<GameObject> objectPool;
    private double shootTimer;
    private int spriteIndex;
    [SerializeField]
    public TowerScriptableObject _towerStat;

    public string ID
    {
        get { return id; }
        set { id = value; }
    }
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public float Range
    {
        get { return range; }
        set
        {
            range = value;
            RangeIndicator.transform.localScale = new Vector2(Range, Range);
        }
    }
    public virtual double AttackSpeed
    {
        get { return attackSpeed; }
        set { attackSpeed = value; }
    }
    public int Price
    {
        get { return price; }
        set { price = value; }
    }

    public int Size
    {
        get { return size; }
        set { size = value; }
    }
    public Vector3 ShootPosition
    {
        get { return shootPosition; }
        set { shootPosition = value; }
    }
    public GameObject CurrentEnemy
    {
        get { return currentEnemy; }
        set { currentEnemy = value; }
    }
    public List<string> Special { get => special; set => special = value; }
    public int SpriteIndex { get => spriteIndex; set => spriteIndex = value; }
    public int PlacementIndex { get => placementIndex; set => placementIndex = value; }
    public GameObject RangeIndicator { get => rangeIndicator; }
    List<int> priceToUpgrade = new List<int>();
    public List<int> PriceToUpgrade { get => priceToUpgrade; }
    public double ShootTimer { get => shootTimer; set => shootTimer = value; }
    public List<GameObject> Monsters { get => monsters; set => monsters = value; }
    public bool flag = false;

    public virtual void Attack()
    {
        //Vector3.
        GameObject arrowGO;
        try
        {
            arrowGO = objectPool.Get();
        }
        catch (Exception ex)
        {
            return;
        }
        //arrowGO.transform.parent = GameObject.Find("Arrow").transform;
        arrowGO.transform.localScale = new(5, 5, 5);
        arrowGO.transform.position = ShootPosition;
        //arrowGO.transform.localScale = transform.localScale;
        arrowGO.GetComponent<Arrow>().Damage = Damage;
        arrowGO.GetComponent<Arrow>().TargetAiming = CurrentEnemy;
        arrowGO.GetComponent<Arrow>().OnRelease = obj => objectPool.Release(obj);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Minions"))
        {
            Monsters.Add(collision.gameObject);
        }
    }

    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Minions"))
        {
            GameObject monster = collision.gameObject;
            if (monster.GetComponent<AEnemy>().HP <= 0)
            {
                Monsters.Remove(monster);
            }
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Minions"))
        {
            Monsters.Remove(collision.gameObject);
        }
    }

    public virtual void UpdateEnemy()
    {
        if (Monsters.Count > 0)
        {
            CurrentEnemy = Monsters[0];
        }
        else
        {
            CurrentEnemy = null;
        }
    }

    public virtual void Awake()
    {
        if (!flag)
        {
            SetTower();
        }
    }

    public virtual void Start()
    {
        Monsters = new();
        shootTimer = AttackSpeed;
        objectPool = new ObjectPool<GameObject>(() => { return Instantiate(bullet, GameObject.Find("Arrow").transform); }
                                                , obj => { obj.SetActive(true); }
                                                , obj => { obj.SetActive(false); }
                                                , obj => { Destroy(obj); }
                                                , false
                                                , 10
                                                , 20);
    }
    public void SetTower()
    {
        ID = _towerStat._id;
        Damage = _towerStat._attack;
        Range = _towerStat._range;
        AttackSpeed = _towerStat._attackSpeed;
        Price = _towerStat._price;
    }
    public virtual void Update()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            shootTimer = AttackSpeed;
            UpdateEnemy();
            if (CurrentEnemy != null)
            {
                Vector2 direction = CurrentEnemy.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                ShootPosition = transform.GetChild(3).position;
                //transform.GetChild(3).rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                Attack();
            }
            else
            {
                transform.rotation = Quaternion.identity;
                //transform.GetChild(3).rotation = Quaternion.identity;
            }
        }
    }

    public void ChangeSprite(int i)
    {
        throw new NotImplementedException();
    }

    public Sprite GetUpgradeSprite(int i)
    {
        throw new NotImplementedException();
    }
}