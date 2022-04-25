using UnityEngine;
interface IEnemy
{
    void Update(DataPlayer dataPlayer, DataType dataType);
}
class Enemy : IEnemy
{
    private const int KCoins = 5;
    private const float KPower = 1.5f;
    private const int MaxHealthPlayer = 20;
    private string _name;
    private int _moneyPlayer;
    private int _healthPlayer;
    private int _powerPlayer;
    private int _crimeLevel;
    public Enemy(string name)
    {
        _name = name;
    }
    public void Update(DataPlayer dataPlayer, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Money:
                _moneyPlayer = dataPlayer.Money;
                break;
            case DataType.Health:
                _healthPlayer = dataPlayer.Health;
                break;
            case DataType.Power:
                _powerPlayer = dataPlayer.Power;
                break;
            case DataType.Crime:
                _crimeLevel = dataPlayer.Crime;
                break;
        }
        Debug.Log($"Notified {_name} change to {dataPlayer}");
    }
    public int Power
    {
        get
        {
            var kHealth = _healthPlayer > MaxHealthPlayer ? 10 : 2;
            var power = (int)(_moneyPlayer / KCoins + kHealth + _powerPlayer / KPower + _crimeLevel);
            return power;
        }
    }
}