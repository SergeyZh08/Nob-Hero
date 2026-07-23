using System.Collections.Generic;
using UnityEngine;

public class TopIconManager : MonoBehaviour
{
    [SerializeField] private TopIcon[] _activeIcons;
    [SerializeField] private TopIcon[] _passiveIcons;
    private List<ActiveTopIcon> _activeApplied = new List<ActiveTopIcon>();

    private void Update()
    {
        foreach (var icon in _activeApplied)
        {
            icon.Refresh();
        }
    }

    public void Add(Effect effect)
    {
        if (effect as ActiveEffect)
        {
            for (int i = 0; i < _activeIcons.Length; i++)
            {
                if (!_activeIcons[i].CurrentEffect)
                {
                    _activeIcons[i].Apply(effect);
                    _activeApplied.Add((ActiveTopIcon)_activeIcons[i]);
                    return;
                }
            }
        }

        if (effect as PassiveEffect)
        {
            for (int i = 0; i < _passiveIcons.Length; i++)
            {
                if (!_passiveIcons[i].CurrentEffect)
                {
                    _passiveIcons[i].Apply(effect);
                    return;
                }
            }
        }
    }
}
