using System;
using System.Reflection;
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
    private static readonly FieldInfo[] _fields = typeof(LevelStats).GetFields();

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
        _name.text = effect.Name;
        _level.text = $"Lvl: {effect.Level}";

        if (effect is ActiveEffect activeEffect)
        {
            _iconBackgroung.sprite = _continuousSprite;
            _description.text = GetDescription(activeEffect);
        }
        else if (effect is PassiveEffect)
        {
            _iconBackgroung.sprite = _oneTimeSprite;
            _description.text = effect.Description;
        }
    }

    private string GetDescription(ActiveEffect effect)
    {
        if (effect.Level == 0)
        {
            return effect.Description;
        }

        string result = "";

        LevelStats current = effect.Current;
        LevelStats nextLevel = effect.NextLevel;
       
        foreach (var field in _fields)
        {
            object currentField = field.GetValue(current);
            object nextField = field.GetValue(nextLevel);

            var customName = field.GetCustomAttribute<StatNameAttribute>();

            if (!Equals(currentField, nextField))
            {
                float c_field = Convert.ToSingle(currentField);
                float n_field = Convert.ToSingle(nextField);

                result += customName.Name + ": " + (n_field - c_field).ToString("+0.##;-0.##") + "\n";
            }
        }


        return result;
    }

    public void SelectCard()
    {
        _onCardSelected?.Invoke(_effect);
    }
}
