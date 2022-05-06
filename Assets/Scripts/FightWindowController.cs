using Profile;
using UnityEngine;
public class FightWindowController : BaseController
{
    private FightWindowView _fightWindowViewInstance;
    private ProfilePlayer _profilePlayer;
    private int _allCountMoneyPlayer;
    private int _allCountHealthPlayer;
    private int _allCountForcePlayer;
    private Money _money;
    private Health _heath;
    private Force _force;
    private Enemy _enemy;
    public FightWindowController(Transform placeForUi, FightWindowView fightWindowView,
    ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _fightWindowViewInstance = GameObject.Instantiate(fightWindowView, placeForUi);
    }
    public void RefreshView()
    {
        _enemy = new Enemy("Enemy Flappy");
        _money = new Money(nameof(Money));
        _money.Attach(_enemy);
        _heath = new Health(nameof(Health));
        _heath.Attach(_enemy);
        _force = new Force(nameof(Force));
        _force.Attach(_enemy);
        SubscribeButtons();
    }
    private void SubscribeButtons()
    {
        _fightWindowViewInstance.AddCoinsButton.onClick.AddListener(() =>
        ChangeMoney(true));
        _fightWindowViewInstance.MinusCoinsButton.onClick.AddListener(() =>
        ChangeMoney(false));
        _fightWindowViewInstance.AddHealthButton.onClick.AddListener(() =>
        ChangeHealth(true));
        _fightWindowViewInstance.MinusHealthButton.onClick.AddListener(() =>
        ChangeHealth(false));
        _fightWindowViewInstance.AddPowerButton.onClick.AddListener(() =>
        ChangeForce(true));
        _fightWindowViewInstance.MinusPowerButton.onClick.AddListener(() =>
        ChangeForce(false));
        _fightWindowViewInstance.FightButton.onClick.AddListener(Fight);
        _fightWindowViewInstance.LeaveFightButton.onClick.AddListener(CloseWindow);
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
    private void ChangeForce(bool isAddCount)
    {
        if (isAddCount)
            _allCountForcePlayer++;
        else
            _allCountForcePlayer--;
        ChangeDataWindow(_allCountForcePlayer, DataType.Force);
    }
    private void Fight()
    {
        Debug.Log(_allCountForcePlayer >= _enemy.Force
        ? "<color=#07FF00>Win!!!</color>"
        : "<color=#FF0000>Lose!!!</color>");
    }
    private void ChangeDataWindow(int countChangeData, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Money:
                _fightWindowViewInstance.CountMoneyText.text = $"Player Money { countChangeData.ToString()}";
                _money.Money = countChangeData;
                break;
            case DataType.Health:
                _fightWindowViewInstance.CountHealthText.text = $"Player Health { countChangeData.ToString()}";
                _heath.Health = countChangeData;
                break;
            case DataType.Force:
                _fightWindowViewInstance.CountPowerText.text = $"Player Force { countChangeData.ToString()}";
                _force.Force = countChangeData;
                break;
        }
        _fightWindowViewInstance.CountPowerEnemyText.text = $"Enemy Force { _enemy.Force}";
    }
    private void CloseWindow()
    {
        _profilePlayer.CurrentState.Value = GameState.Game;
    }
    protected override void OnDispose()
    {
        _fightWindowViewInstance.AddCoinsButton.onClick.RemoveAllListeners();
        _fightWindowViewInstance.MinusCoinsButton.onClick.RemoveAllListeners();
        _fightWindowViewInstance.AddHealthButton.onClick.RemoveAllListeners();
        _fightWindowViewInstance.MinusHealthButton.onClick.RemoveAllListeners();
        _fightWindowViewInstance.AddPowerButton.onClick.RemoveAllListeners();
        _fightWindowViewInstance.MinusPowerButton.onClick.RemoveAllListeners();
        _fightWindowViewInstance.FightButton.onClick.RemoveAllListeners();
        _fightWindowViewInstance.LeaveFightButton.onClick.RemoveAllListeners();
        _money.Detach(_enemy);
        _heath.Detach(_enemy);
        _force.Detach(_enemy);
        GameObject.Destroy(_fightWindowViewInstance.gameObject);
        base.OnDispose();
    }
}