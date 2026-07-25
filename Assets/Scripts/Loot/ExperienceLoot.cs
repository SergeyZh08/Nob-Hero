using UnityEngine;

public class ExperienceLoot : Loot
{
    [SerializeField] private int _experienceCount = 1;
    protected override void Take(Player player)
    {
        base.Take(player);
        player.Experience.AddExperience(_experienceCount);
    }
}
