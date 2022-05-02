using UnityEngine;

namespace Assets.Scripts
{
    public class CurrencyController
    {
        private CurrencyView _currencyViewInstance;
        public CurrencyController(Transform placeForUi, CurrencyView currencyView)
        {
            _currencyViewInstance = GameObject.Instantiate(currencyView, placeForUi);
        }
        public void CloseWindow()
        {
            GameObject.Destroy(_currencyViewInstance.gameObject);
        }
    }
}
