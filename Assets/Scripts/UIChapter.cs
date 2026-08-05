using TMPro;
using UnityEngine;

public class UIChapter : MonoBehaviour
{
    [SerializeField] private ChapterManager _chapterManager;
    [SerializeField] private TextMeshProUGUI _chapterText;

    private void Start()
    {
        _chapterManager.OnNextChapter += SetValue;

        SetValue(_chapterManager.CurrentChapter);
    }

    private void OnDestroy()
    {
        _chapterManager.OnNextChapter -= SetValue;
    }

    private void SetValue(int value)
    {
        _chapterText.text = $"Chapter {value + 1}";
    }
}
