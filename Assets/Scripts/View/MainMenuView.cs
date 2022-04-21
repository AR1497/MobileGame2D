using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField]
    private Button _startButton;
    [SerializeField] private Button _buttonInventory;

    private void OnDestroy()
    {
        _startButton.onClick.RemoveAllListeners();
    }

    internal void Init(UnityAction start, UnityAction showInventoryAction)
    {
        _startButton.onClick.AddListener(start);
        _buttonInventory.onClick.AddListener(showInventoryAction);
    }
}
