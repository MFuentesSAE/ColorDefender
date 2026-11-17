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
        }
    }

    void ShowAd()
    {
        if (!showAd)
        {
            return;
        }

        Monou.GameScraper.Advertise();
    }

}
