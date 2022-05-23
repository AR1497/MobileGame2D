using Profile;
using UnityEngine;
public class StartFightController : BaseController
{
    private StartFightView _startFightViewInstance;
    private ProfilePlayer _profilePlayer;
    public StartFightController(Transform placeForUi, StartFightView startFightView,
    ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _startFightViewInstance = GameObject.Instantiate(startFightView, placeForUi);
    }
    public void RefreshView()
    {
        _startFightViewInstance.StartFightButton.onClick.AddListener(StartFight);
    }
    private void StartFight()
    {
        _profilePlayer.CurrentState.Value = GameState.Fight;
    }
    protected override void OnDispose()
    {
        _startFightViewInstance.StartFightButton.onClick.RemoveAllListeners();
        GameObject.Destroy(_startFightViewInstance.gameObject);
        base.OnDispose();
    }
}
