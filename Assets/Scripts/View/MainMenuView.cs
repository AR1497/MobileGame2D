using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField]
    private Button _startButton;

    private void OnDestroy()
    {
        _startButton.onClick.RemoveAllListeners();
    }

    internal void Init(UnityAction start)
    {
        _startButton.onClick.AddListener(start);
    }
}
