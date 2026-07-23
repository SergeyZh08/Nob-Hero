using UnityEngine;

[System.Serializable]
public struct EnemyWave
{
    public Enemy Enemy;
    public float[] NumberPerSecund;
}

[CreateAssetMenu (fileName = "Chapter")]
public class ChapterSettings : ScriptableObject
{
    public EnemyWave[] EnemyWaves;
}
