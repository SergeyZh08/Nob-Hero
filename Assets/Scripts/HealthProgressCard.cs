using UnityEngine;

public class HealthProgressCard : ProgressCard
{
    protected override void OnBought()
    {
        Player.Stats.AddPermanentHealth(Config.Percent);
        Player.Health.ApplyStats();
        base.OnBought();
    }
}
