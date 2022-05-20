using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class LoadWindowView : AssetBundleViewBase
    {
        [SerializeField]
        private Button _loadAssetsButton;

        private void Start()
        {
            _loadAssetsButton.onClick.AddListener(LoadAssets);
        }

        private void OnDestroy()
        {
            _loadAssetsButton.onClick.RemoveAllListeners();
        }

        private void LoadAssets()
        {
            _loadAssetsButton.interactable = false;
            StartCoroutine(DownloadAndSetAssetBundle());
        }
    }
}