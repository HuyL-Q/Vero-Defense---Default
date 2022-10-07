using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class AEnemy : MonoBehaviour, IEnemy
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const string Magnitude = "Magnitude";
    private const string Dead = "Dead";
    NavMeshAgent _agent;
    Animator _animator;
    Animation _animation;
    GameObject _destination;
    //private int wavepointIndex;
    //string id;
    [SerializeField]
    float _hp;
    //float runSpeed;
    int _damageToCastle;
    int _reward;
    //int size;
    //private bool dead;
    //public string Id { get => id; set => id = value; }
    public float HP { get => _hp; set => _hp = value; }
    public NavMeshAgent Agent { get => _agent; set => _agent = value; }
    public Animator Animator { get => _animator; set => _animator = value; }

    //public float RunSpeed { get => runSpeed; set => runSpeed = value; }
    public int DamageToCastle { get => _damageToCastle; set => _damageToCastle = value; }
    public int Reward { get => _reward; set => _reward = value; }
    public Animation Animation { get => _animation; set => _animation = value; }

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
        if (Agent.remainingDistance <= 1)
        {
            Agent.isStopped = true;
            AttackToCastle();
        }
    }


    public abstract IEnumerator SetEnemy(string id);

    public void ReceiveDamage(float dmg)
    {
        if(gameObject.TryGetComponent<Vulnerable>(out Vulnerable eff))
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
            foreach(AEffect effect in gameObject.GetComponents<AEffect>())
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
        //throw new System.NotImplementedException();
        GameController.instance.PlayerLives -= DamageToCastle;
        StoryUIController.instance.UpdateLivesIndex();
        NewSpawnController.Instance.CurrentNumOfEnemies--;
        foreach (AEffect effect in gameObject.GetComponents<AEffect>())
        {
            effect.enabled = false;
        }
        gameObject.SetActive(false);
    }

    public void GiveReward()
    {
        GameController.instance.PlayerMoney += Reward;
        GameController.instance.PlayerPoint += Mathf.Ceil(Reward * 1.5f);
        StoryUIController.instance.UpdateGoldIndex();
        //throw new System.NotImplementedException();
    }

    public void Die()
    {
        GiveReward();
        foreach (AEffect effect in gameObject.GetComponents<AEffect>())
        {
            effect.enabled = false;
        }
        NewSpawnController.Instance.CurrentNumOfEnemies--;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(SetEnemy("enemy_01_1"));
        Agent = GetComponent<NavMeshAgent>();
        _destination = GameObject.Find("Destination");
        Agent.destination = _destination.transform.position;
    }
}