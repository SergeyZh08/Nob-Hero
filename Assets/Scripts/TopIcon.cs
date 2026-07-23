using UnityEngine;
using UnityEngine.UI;

public class TopIcon : MonoBehaviour
{
    public Effect CurrentEffect {get; private set;}
    [SerializeField] private Image _lockImage;
    [SerializeField] protected Image _iconImage;
    [SerializeField] private Image _backGround;

    public virtual void Apply(Effect effect)
    {
        CurrentEffect = effect;
        _iconImage.sprite = effect.Sprite;
        _lockImage.gameObject.SetActive(false);
        _iconImage.gameObject.SetActive(true);
        _backGround.gameObject.SetActive(true);
    }
}
