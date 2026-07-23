using UnityEngine;
using UnityEngine.UI;

public class ActiveTopIcon : TopIcon
{
    [SerializeField] private Image _alphaImage;

    public override void Apply(Effect effect)
    {
        base.Apply(effect);
        _alphaImage.sprite = effect.Sprite;
        _alphaImage.gameObject.SetActive(true);
    }

    public void Refresh()
    {
        _iconImage.fillAmount = CurrentEffect.CooldownProgress;
    }
}
