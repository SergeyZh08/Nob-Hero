using UnityEngine;

[System.Serializable]
public struct LootItem
{
    public Loot Loot;
    public float Weight;
}

[CreateAssetMenu (fileName = nameof(LootDrop), menuName = "Loot/" + nameof(LootDrop))]
public class LootDrop : ScriptableObject
{
    public LootItem[] Items;

    public LootItem GetItem()
    {
        float totalWeigth = 0;

        foreach (var item in Items)
        {
            totalWeigth += item.Weight;
        }

        float randomWeight = Random.Range(0, totalWeigth);


        foreach (var item in Items)
        {
            if (randomWeight < item.Weight)
            {
                return item;
            }

            randomWeight -= item.Weight;
        }

        return new LootItem();
    }
}
