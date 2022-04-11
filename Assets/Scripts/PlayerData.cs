using Tools;

public class PlayerData
{
    public PlayerData()
    {
        Car = new Car(1f);
        GameState = new SubscriptionProperty<GameState>();
        GameState.Value = global::GameState.None;
    }

    public Car Car { get; }
    public SubscriptionProperty<GameState> GameState { get; set; }
}
