using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image _iconBackgroung;
    [SerializeField] private Image _iconImage;

    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private Button _button;

    [SerializeField] private Sprite _continuousSprite;
    [SerializeField] private Sprite _oneTimeSprite;

    private Action<Effect> _onCardSelected;
    
    private Effect _effect;

    public void Init(Action<Effect> onCardSelected)
    {
        _onCardSelected = onCardSelected;
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(SelectCard);
    }

    public void Show(Effect effect)
    {
        _effect = effect;

        _iconImage.sprite = effect.Sprite;
        _name.text = effect.name;
        _description.text = effect.Description;
        _level.text = $"Lvl: {effect.Level}";

        if (effect is ActiveEffect)
        {
            _iconBackgroung.sprite = _continuousSprite;
        }
        else if (effect is PassiveEffect)
        {
            _iconBackgroung.sprite = _oneTimeSprite;
        }
    }

    public void SelectCard()
    {
        _onCardSelected?.Invoke(_effect);
    }
}
