using UnityEngine;

public class ButtonsController : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject continueMenu;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject levelSelectMenu;

    [SerializeField] private GameObject mainMenu;


    public void OptionsMenu()
    {
        optionsMenu.SetActive(true);

        continueMenu.SetActive(false);
        creditsMenu.SetActive(false);
        levelSelectMenu.SetActive(false);
        mainMenu.SetActive(false);
    }

    public void CreditsMenu()
    {
        optionsMenu.SetActive(false);

        continueMenu.SetActive(false);
        creditsMenu.SetActive(true);
        levelSelectMenu.SetActive(false);
        mainMenu.SetActive(false);
    }

    public void LevelSelectMenu()
    {
        optionsMenu.SetActive(false);

        continueMenu.SetActive(false);
        creditsMenu.SetActive(false);
        levelSelectMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void ContinueMenu()
    {
        optionsMenu.SetActive(false);

        continueMenu.SetActive(true);
        creditsMenu.SetActive(false);
        levelSelectMenu.SetActive(false);
        mainMenu.SetActive(false);
    }

    public void ReturnMainMenu()
    {
        optionsMenu.SetActive(false);

        continueMenu.SetActive(false);
        creditsMenu.SetActive(false);
        levelSelectMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

}
