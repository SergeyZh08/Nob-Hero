using UnityEngine;

public class WaweManager : MonoBehaviour, ISaved
{
    private int _currentWawe;

    public void LoadFrom(SaveData data)
    {
        _currentWawe = data.Wawe;
    }

    public void SaveTo(SaveData data)
    {
        data.Wawe = _currentWawe;
    }
}
