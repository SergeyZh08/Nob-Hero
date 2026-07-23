using UnityEngine;

public class ExperienceLoot : Loot
{
    [SerializeField] private int _experienceCount = 1;
    protected override void Take(Collector collector)
    {
        base.Take(collector);
        collector.TakeExperience(_experienceCount);
    }
}
