using UnityEngine;

public class DamageProgressCard : ProgressCard
{
    protected override void OnBought()
    {
        Player.Stats.AddPermanentDamage(Config.Percent);
        base.OnBought();
    }
}
