using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinWindow : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void Awake()
    {
        _button.onClick.AddListener(RestartGame);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
