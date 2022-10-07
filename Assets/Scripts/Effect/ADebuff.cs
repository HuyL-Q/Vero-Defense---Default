using UnityEngine;

public class ADebuff : AEffect
{
    public override void OnDisable()
    {
        DeactiveAnimation(this.GetType().Name);
        CurrentDuration = 0;
    }

    // Start is called before the first frame update
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

    public override void OnEnable()
    {
        Particle = Resources.Load<GameObject>("Animations/" + this.GetType().Name);
        ActiveAnimation(this.GetType().Name);
        ResetDuration();
    }
}
