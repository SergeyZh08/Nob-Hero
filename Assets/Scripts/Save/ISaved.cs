using UnityEngine;

public interface ISaved
{
    public void SaveTo(SaveData data);
    public void LoadFrom(SaveData data);
}
