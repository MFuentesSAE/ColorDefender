using UnityEngine;

public class AdManager : MonoBehaviour
{
    private bool showAd = false;
    public static AdManager instance;

    public void ToggleShowAd(bool toggle)
    {
        showAd = toggle;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // El MonouGameScraper.jslib oficial envia el resultado del anuncio con SendMessage('GameScraper', ...)
            gameObject.name = "GameScraper";
        }
    }

    public void ShowAd()
    {
        if (!showAd)
        {
            return;
        }

        Monou.GameScraper.Advertise();
    }

}
