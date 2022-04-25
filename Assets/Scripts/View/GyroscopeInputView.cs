//using JoostenProductions;
//using Tools;
//using UnityEngine;
//namespace Game.InputLogic
//{
//    internal class GyroscopeInputView : BaseInputView
//    {
//        public override void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, float speed)
//        {
//            base.Init(leftMove, rightMove, speed);
//            Input.gyro.enabled = true;
//            UpdateManager.SubscribeToUpdate(Move);
//        }
//        private void OnDestroy()
//        {
//            UpdateManager.UnsubscribeFromUpdate(Move);
//        }
//        private void Move()
//        {
//            if (!SystemInfo.supportsGyroscope)
//                return;
//            Quaternion quaternion = Input.gyro.attitude;
//            quaternion.Normalize();
//            OnRightMove((quaternion.x + quaternion.y) * Time.deltaTime * _speed);
//        }
//    }
//}