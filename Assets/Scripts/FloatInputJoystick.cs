using JoostenProductions;
using Tools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;


public class FloatInputJoystick : BaseInputView, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    #region Fields

    [SerializeField] private Joystick _joystick;
    [SerializeField] private GameObject _joystickView;
    private CanvasGroup _container;

    private Vector3 _defaultPosition;

    private bool _usedJoystick;

    #endregion

    #region Methods

    public override void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, float speed)
    {
        base.Init(leftMove, rightMove, speed);
        _defaultPosition = transform.position;
        _joystickView.SetActive(false);
        Debug.Log($"On Init:{_joystickView.activeSelf}");
        UpdateManager.SubscribeToUpdate(Move);
    }

    private void OnDestroy()
    {
        UpdateManager.UnsubscribeFromUpdate(Move);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _joystick.transform.position = eventData.position;
        _joystick.SetStartPosition(eventData.position);
        _joystick.OnPointerDown(eventData);
        _usedJoystick = true;
        _joystickView.transform.position = eventData.position;
        Debug.Log($"BeforeSetActive:{_joystickView.activeSelf}");
        _joystickView.SetActive(true);
        Debug.Log($"AfterSetActive:{_joystickView.activeSelf}");
        _container.alpha = 1;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _usedJoystick = false;
        transform.position = _defaultPosition;
        _container.alpha = 0;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _joystick.OnDrag(eventData);
    }

    private void Move()
    {
        Debug.Log($"OnMove:{_joystickView.activeSelf}");
        if (_usedJoystick)
        {
            float moveStep = 10 * Time.deltaTime * CrossPlatformInputManager.GetAxis("Horizontal");
            if (moveStep > 0)
                OnRightMove(moveStep);
            else if (moveStep < 0)
                OnLeftMove(moveStep);
        }
    }

    #endregion
}