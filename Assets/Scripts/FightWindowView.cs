using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class FightWindowView : MonoBehaviour
{
    [SerializeField]
    private Text _countMoneyText;
    [SerializeField]
    private Text _countHealthText;
    [SerializeField]
    private Text _countPowerText;
    [SerializeField]
    private Text _countPowerEnemyText;
    [SerializeField]
    private Button _addCoinsButton;
    [SerializeField]
    private Button _minusCoinsButton;
    [SerializeField]
    private Button _addHealthButton;
    [SerializeField]
    private Button _minusHealthButton;
    [SerializeField]
    private Button _addPowerButton;
    [SerializeField]
    private Button _minusPowerButton;
    [SerializeField]
    private Button _fightButton;
    [SerializeField]
    private Button _leaveFightButton;
    public Text CountMoneyText => _countMoneyText;
    public Text CountHealthText => _countHealthText;
    public Text CountPowerText => _countPowerText;
    public Text CountPowerEnemyText => _countPowerEnemyText;
    public Button AddCoinsButton => _addCoinsButton;
    public Button MinusCoinsButton => _minusCoinsButton;
    public Button AddHealthButton => _addHealthButton;
    public Button MinusHealthButton => _minusHealthButton;
    public Button AddPowerButton => _addPowerButton;
    public Button MinusPowerButton => _minusPowerButton;
    public Button FightButton => _fightButton;
    public Button LeaveFightButton => _leaveFightButton;
}