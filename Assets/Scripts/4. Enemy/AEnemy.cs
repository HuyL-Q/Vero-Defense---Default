using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class AEnemy : MonoBehaviour, IEnemy
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const string Magnitude = "Magnitude";
    private const string Dead = "Dead";
    public EnemyScriptableObject data;
    NavMeshAgent _agent;
    Animator _animator;
    Animation _animation;
    [SerializeField]
    float _hp;
    public Slider HealthAmountUI;
    float maxHP;
    List<string> effect;
    //float runSpeed;
    int _damageToCastle;
    int _reward;
    float speed;
    string id;

    public float HP { get => _hp; set => _hp = value; }
    public NavMeshAgent Agent { get => _agent; set => _agent = value; }
    public Animator Animator { get => _animator; set => _animator = value; }

    //public float RunSpeed { get => runSpeed; set => runSpeed = value; }
    public int DamageToCastle { get => _damageToCastle; set => _damageToCastle = value; }
    public int Reward { get => _reward; set => _reward = value; }
    public Animation Animation { get => _animation; set => _animation = value; }
    public float MaxHP { get => maxHP; set => maxHP = value; }
    public List<string> Effect { get => effect; set => effect = value; }
    public string Id { get => id; set => id = value; }
    public float Speed { get => speed; set => speed = value; }

    public virtual void Awake()
    {
        Id = this.GetType().Name;
    }

    public virtual void Start()
    {
        Animation = GetComponent<Animation>();
        Animator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
        //MaximumHealth = Hp;
        //Dead = false;
    }

    public virtual void Update()
    {
        Animator.SetFloat(Magnitude, Agent.velocity.magnitude);
        Animator.SetFloat(Horizontal, Agent.velocity.x);
        Animator.SetFloat(Vertical, Agent.velocity.y);
        if (Agent.remainingDistance <= 0)
        {
            Agent.isStopped = true;
            AttackToCastle();
        }
        HealthAmountUI.value = HP / MaxHP;
    }

    public void SetEnemy(EnemyScriptableObject? dat = null)
    {
        if(dat != null)
        {
            data = dat;
        }
        Id = data.id;
        HP = data.hp;
        DamageToCastle = data.damage;
        Reward = data.killReward;
        MaxHP = HP;
        Effect = data.Effect;
        Speed = data.speed;
        List<Type> temp = new List<Type>();
        foreach(string ef in Effect)
        {
            temp.Add(Type.GetType(ef));
        }
        AEffect.Active(gameObject,temp);
    }

    public void ReceiveDamage(float dmg)
    {
        if (gameObject.TryGetComponent<Vulnerable>(out Vulnerable eff))
        {
            if (eff.enabled)
            {
                //set additiondamage here
                eff.SetAdditionDamage(ref dmg);
            }
        }
        HP -= dmg;
        if (HP <= 0)
        {
            foreach (AEffect effect in gameObject.GetComponents<AEffect>())
            {
                effect.enabled = false;
            }
            //NewSpawnController.Instance.CurrentNumOfEnemies--;
            Agent.isStopped = true;
            //GiveReward();
            Animator.SetTrigger(Dead);
        }
    }

    public void AttackToCastle()
    {
        GameController.instance.PlayerLives -= DamageToCastle;
        StoryUIController.instance.UpdateLivesIndex();
        NewSpawnController.Instance.CurrentNumOfEnemies--;
        foreach (AEffect effect in gameObject.GetComponents<AEffect>())
        {
            effect.enabled = false;
        }
        ObjectPoolController.Instance.Pools[this.id].Release(gameObject);
    }

    public void GiveReward()
    {
        GameController.instance.PlayerMoney += Reward;
        GameController.instance.PlayerPoint += Mathf.Ceil(Reward * 1.5f);
        StoryUIController.instance.UpdateGoldIndex();
    }

    public void Die()
    {
        GiveReward();
        foreach (AEffect effect in gameObject.GetComponents<AEffect>())
        {
            effect.enabled = false;
        }
        NewSpawnController.Instance.CurrentNumOfEnemies--;
        ObjectPoolController.Instance.Pools[this.id].Release(gameObject);
    }

    public virtual void OnEnable()
    {
        SetEnemy();
        Agent = GetComponent<NavMeshAgent>();
        //_destination = GameObject.Find("Destination");
        //Agent.destination = _destination.transform.position;
        Agent.speed = Speed;
    }
}