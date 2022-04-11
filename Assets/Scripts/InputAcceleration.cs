using JoostenProductions;
using Tools;
using Profile;
using UnityEngine;

internal class InputAcceleration : BaseInputView
{
    public void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, float speed)
    {
        base.Init(leftMove, rightMove, speed);
        UpdateManager.SubscribeToUpdate(Move);
    }

    private void OnDestroy()
    {
        UpdateManager.UnsubscribeFromUpdate(Move);
    }

    private void Move()
    {
        Vector3 direction = Vector3.zero;
        direction.x = -Input.acceleration.y;
        direction.z = Input.acceleration.x;

        if (direction.sqrMagnitude > 1)
            direction.Normalize();

        if (direction.x > 0)
            OnRightMove(direction.sqrMagnitude / 20 * _speed * direction.x);
        else if (direction.x < 0)
            OnLeftMove(direction.sqrMagnitude / 20 * _speed * direction.x);
    }
}

