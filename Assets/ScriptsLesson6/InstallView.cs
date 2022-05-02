using UnityEngine;
public class InstallView : MonoBehaviour
{
    [SerializeField]
    private DailyRewardView _dailyRewardView;
    private DailyRewardController _dailyRewardController;
    private Transform _placeForUi;
    private CurrencyView _currencyView;

    private void Awake()
    {
        _dailyRewardController = new DailyRewardController(_placeForUi, _dailyRewardView, _currencyView);
    }
    private void Start()
    {
        _dailyRewardController.RefreshView();
    }
}