using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
public class LocalizationWindow : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _changeText;
    [SerializeField]
    private Button _russianButton;
    [SerializeField]
    private Button _englishButton;
    [SerializeField]
    private Button _frenchButton;
    private void Start()
    {
        ChangedLocaleEvent(null);
        LocalizationSettings.SelectedLocaleChanged += ChangedLocaleEvent;
        _russianButton.onClick.AddListener(() => ChangeLanguage(2));
        _frenchButton.onClick.AddListener(() => ChangeLanguage(1));
        _englishButton.onClick.AddListener(() => ChangeLanguage(0));
    }
    private void OnDestroy()
    {
        _russianButton.onClick.RemoveAllListeners();
        _frenchButton.onClick.RemoveAllListeners();
        _englishButton.onClick.RemoveAllListeners();
    }
    private void ChangedLocaleEvent(Locale locale)
    {
        StartCoroutine(OnChangedLocale(locale));
    }
    private IEnumerator OnChangedLocale(Locale locale)
    {
        var loadingOperation =
        LocalizationSettings.StringDatabase.GetTableAsync("Font");
        yield return loadingOperation;
        if (loadingOperation.Status == AsyncOperationStatus.Succeeded)
        {
            var table = loadingOperation.Result;
            _changeText.text = table.GetEntry("translate_text")?.GetLocalizedString();
        }
        else
        {
            Debug.Log("Could not load String Table\n" +
            loadingOperation.OperationException);
        }
    }
    private void ChangeLanguage(int index)
    {
        LocalizationSettings.SelectedLocale =
        LocalizationSettings.AvailableLocales.Locales[index];
    }
}
