using TMPro;
using UnityEngine;

public class Fps : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _fps;
    [SerializeField] private float _updateInterval = 0.5f; // Как часто обновлять текст

    private float _accumulatedTime = 0f;
    private int _frameCount = 0;

    private void Update()
    {
        _accumulatedTime += Time.unscaledDeltaTime;
        _frameCount++;

        // Обновляем текст только по истечении интервала
        if (_accumulatedTime >= _updateInterval)
        {
            int fps = Mathf.RoundToInt(_frameCount / _accumulatedTime);
            
            // Используем SetText вместо .text = ... чтобы не создавать лишний мусор в памяти
            _fps.SetText("{0} FPS", fps); 

            // Сбрасываем счетчики для следующего замера
            _accumulatedTime = 0f;
            _frameCount = 0;
        }
    }
}