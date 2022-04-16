using Tools;

public class GameController : BaseController
{
    public GameController(PlayerData model)
    {
        var leftMoveDiff = new SubscriptionProperty<float>();
        var rightMoveDiff = new SubscriptionProperty<float>();

        var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
        AddController(tapeBackgroundController);

        var inputGameController = new InputController(leftMoveDiff, rightMoveDiff, model.CurrentCar);
        AddController(inputGameController);

        var carController = new CarController();
        AddController(carController);
    }
}
