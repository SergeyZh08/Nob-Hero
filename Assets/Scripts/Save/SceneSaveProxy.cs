using UnityEngine;

public class SceneSaveProxy : MonoBehaviour
{
    [SerializeField] private PlayerHealth _player;
    //[SerializeField] private Inventory _inventory;

    private void Start()
    {
        //SaveManager.Instance.RegisterAndLoad(_player, _inventory, LevelManager.Instance);
    }
}
