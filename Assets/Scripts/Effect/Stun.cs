public class Stun : ADebuff
{
    //Start is called before the first frame update
    private void Awake()
    {
        Duration = 10;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    public override void OnDisable()
    {
        gameObject.GetComponent<AEnemy>().Agent.isStopped = false;
        base.OnDisable();
    }
    public override void OnEnable()
    {
        base.OnEnable();
        gameObject.GetComponent<AEnemy>().Agent.isStopped = true;
    }
}
