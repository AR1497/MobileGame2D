using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MainWindowObserver : MonoBehaviour
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
    private Text _countCrimeLevelText;
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
    private Button _addCrimeLevelButton;
    [SerializeField]
    private Button _minusCrimeLevelButton;
    [SerializeField]
    private Button _fightButton;
    [SerializeField]
    private Button _passButton;
    private int _allCountMoneyPlayer;
    private int _allCountHealthPlayer;
    private int _allCountPowerPlayer;
    private int _allCountCrimeLevel;
    private Money _money;
    private Health _heath;
    private Power _power;
    private Enemy _enemy;
    private CrimeLevel _crime;
    private void Start()
    {
        _enemy = new Enemy("Enemy Flappy");
        _money = new Money(nameof(Money));
        _money.Attach(_enemy);
        _heath = new Health(nameof(Health));
        _heath.Attach(_enemy);
        _power = new Power(nameof(Power));
        _power.Attach(_enemy);
        _crime = new CrimeLevel(nameof(CrimeLevel));
        _addCoinsButton.onClick.AddListener(() => ChangeMoney(true));
        _minusCoinsButton.onClick.AddListener(() => ChangeMoney(false));
        _addHealthButton.onClick.AddListener(() => ChangeHealth(true));
        _minusHealthButton.onClick.AddListener(() => ChangeHealth(false));
        _addPowerButton.onClick.AddListener(() => ChangePower(true));
        _minusPowerButton.onClick.AddListener(() => ChangePower(false));
        _addCrimeLevelButton.onClick.AddListener(() => ChangeCrimeLevel(true));
        _minusCrimeLevelButton.onClick.AddListener(() => ChangeCrimeLevel(false));
        _fightButton.onClick.AddListener(Fight);
        _passButton.onClick.AddListener(Pass);
    }
    private void OnDestroy()
    {
        _addCoinsButton.onClick.RemoveAllListeners();
        _minusCoinsButton.onClick.RemoveAllListeners();
        _addHealthButton.onClick.RemoveAllListeners();
        _minusHealthButton.onClick.RemoveAllListeners();
        _addPowerButton.onClick.RemoveAllListeners();
        _minusPowerButton.onClick.RemoveAllListeners();
        _addCrimeLevelButton.onClick.RemoveAllListeners();
        _minusCrimeLevelButton.onClick.RemoveAllListeners();
        _fightButton.onClick.RemoveAllListeners();
        _passButton.onClick.RemoveAllListeners();
        _money.Detach(_enemy);
        _heath.Detach(_enemy);
        _power.Detach(_enemy);
    }
    private void ChangeMoney(bool isAddCount)
    {
        if (isAddCount)
            _allCountMoneyPlayer++;
        else
            _allCountMoneyPlayer--;
        ChangeDataWindow(_allCountMoneyPlayer, DataType.Money);
    }
    private void ChangeHealth(bool isAddCount)
    {
        if (isAddCount)
            _allCountHealthPlayer++;
        else
            _allCountHealthPlayer--;
        ChangeDataWindow(_allCountHealthPlayer, DataType.Health);
    }
    private void ChangePower(bool isAddCount)
    {
        if (isAddCount)
            _allCountPowerPlayer++;
        else
            _allCountPowerPlayer--;
        ChangeDataWindow(_allCountPowerPlayer, DataType.Power);
    }
    private void ChangeCrimeLevel(bool isAddCount)
    {
        if (isAddCount) { 
            if (_allCountCrimeLevel < 5)
                _allCountCrimeLevel++;
            if (_allCountCrimeLevel <= 2)
                _passButton.gameObject.SetActive(true);
            else _passButton.gameObject.SetActive(false); 
        }
        else {
            if (_allCountCrimeLevel > 0)
            {
                _allCountCrimeLevel--;
                _passButton.gameObject.SetActive(false);
                if (_allCountCrimeLevel <= 2)
                    _passButton.gameObject.SetActive(true);
                else _passButton.gameObject.SetActive(false);
            }
        }
        ChangeDataWindow(_allCountCrimeLevel, DataType.Crime);
    }
    private void Fight()
    {
        Debug.Log(_allCountPowerPlayer >= _enemy.Power
        ? "<color=#07FF00>Win!!!</color>"
        : "<color=#FF0000>Lose!!!</color>");
    }
    private void Pass()
    {
        Debug.Log(_allCountCrimeLevel <= 2
        ? "<color=#07FF00>Pss!!!</color>"
        : "<color=#FF0000>NoPass!!!</color>");
    }
    private void ChangeDataWindow(int countChangeData, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Money:
                _countMoneyText.text = $"Player Money {countChangeData.ToString()}";
                _money.Money = countChangeData;
                break;
            case DataType.Health:
                _countHealthText.text = $"Player Health {countChangeData.ToString()}";
                _heath.Health = countChangeData;
                break;
            case DataType.Power:
                _countPowerText.text = $"Player Power {countChangeData.ToString()}";
                _power.Power = countChangeData;
                break;
            case DataType.Crime:
                _countCrimeLevelText.text = $"Level Crime {countChangeData.ToString()}";
                _crime.Crime = countChangeData;
                break;
        }
        _countPowerEnemyText.text = $"Enemy Power {_enemy.Power}";
    }
}