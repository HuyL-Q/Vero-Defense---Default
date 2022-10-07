using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABuff : AEffect
{
    List<AEnemy> effected = new List<AEnemy>();

    public List<AEnemy> Effected { get => effected; set => effected = value; }

    public override void OnDisable()
    {
        DeactiveAnimation(this.GetType().Name);
        CurrentDuration = 0;
        //if (Animation != null) Animation.Stop();
    }

    public override void OnEnable()
    {
        Particle = Resources.Load<GameObject>("Animations/" + this.GetType().Name);
        //ActiveAnimation(this.GetType().Name);
        ResetDuration();
    }


    //Start is called before the first frame update
    override public void Start()
    {
        ResetDuration();
    }

    // Update is called once per frame
    override public void Update()
    {
        CurrentDuration -= Time.deltaTime;
        if (CurrentDuration <= 0)
            this.enabled = false;
    }
    public abstract void Affect(AEnemy enemy);
}
