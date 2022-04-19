using Tools;
using Profile;
using Tools.Ads;
using Model.Analytic;

public class PlayerData
{
    public PlayerData(float speedCar, IAdsShower adsShower, IAnalyticTools analyticTools)
    {
        CurrentState = new SubscriptionProperty<GameState>();
        CurrentCar = new Car(speedCar);
        AdsShower = adsShower;
        AnalyticTools = analyticTools;
    }

    public Car CurrentCar { get; }
    public SubscriptionProperty<GameState> CurrentState { get; }
    public IAdsShower AdsShower { get; }
    public IAnalyticTools AnalyticTools { get; }
}
