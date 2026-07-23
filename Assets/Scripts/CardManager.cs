using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private GameObject _cardParent;
    [SerializeField] private Card[] _cards;
    private EffectManager _effectManager;
    private GameStateManager _gameStateManager;

    public void Init(EffectManager effectManager, GameStateManager gameStateManager)
    {
        _effectManager = effectManager;
        _gameStateManager = gameStateManager;

        for (int i = 0; i < _cards.Length; i++)
        {
            _cards[i].Init(SelectCard);
        }
    }

    public void ShowCards(List<Effect> effects)
    {
        _cardParent.SetActive(true);

        for (int i = 0; i < _cards.Length; i++)
        {
            _cards[i].Show(effects[i]);
        }

        _gameStateManager.SetCardState();
    }

    private void SelectCard(Effect effect)
    {
        _effectManager.AddEffect(effect);
        HideCards();
    }

    public void HideCards()
    {
        _cardParent.SetActive(false);
        _gameStateManager.SetAction();
    }
}
