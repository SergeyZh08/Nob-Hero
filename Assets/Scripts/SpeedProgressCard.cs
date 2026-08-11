using UnityEngine;

public class SpeedProgressCard : ProgressCard
{
    protected override void OnBought()
    {
        Player.Stats.AddPermanentSpeed(Config.Percent);
        base.OnBought();
    }
}
