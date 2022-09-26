public abstract class ASkill
{
    bool activatedSkill;

    public bool ActivatedSkill { get => activatedSkill; set => activatedSkill = value; }

    public abstract void Skill();

    public abstract void ClearSkill();
}
