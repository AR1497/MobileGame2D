using Tools;
using Profile;

public class PlayerData
{
    public PlayerData(float speedCar)
    {
        CurrentState = new SubscriptionProperty<GameState>();
        CurrentCar = new Car(speedCar);
    }

    public Car CurrentCar { get; }
    public SubscriptionProperty<GameState> CurrentState { get; }
}
