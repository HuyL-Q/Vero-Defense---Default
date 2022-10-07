public class Cleanse : ABuff
{
    public override void Affect(AEnemy enemy)
    {
        ADebuff[] debuff = enemy.GetComponents<ADebuff>();
        foreach (ADebuff debuff2 in debuff)
        {
            debuff2.enabled = false;
        }
    }

    // Start is called before the first frame update
    private void Awake()
    {
        Effected.Add(gameObject.GetComponent<AEnemy>());
        Duration = 10;
    }
    public override void Update()
    {
        base.Update();
        if (enabled)
        {
            foreach (var effect in Effected)
            {
                Affect(effect);
            }
        }
    }
}
