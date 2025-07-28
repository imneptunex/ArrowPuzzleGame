using UnityEngine;

public class ButtonsController : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject continueMenu;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject levelSelectMenu;

    [SerializeField] private GameObject mainMenu;

    [SerializeField] private GameObject[] optionMenuButtons;


    public void OptionsMenu()
    {
        optionsMenu.SetActive(true);

        continueMenu.SetActive(false);
        creditsMenu.SetActive(false);
        levelSelectMenu.SetActive(false);
        mainMenu.SetActive(false);

        foreach (GameObject optionMenuButton in optionMenuButtons)
        {
            optionMenuButton.SetActive(true);
        }

        
        
        
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
       

        GameManager.Instance.LoadLastSceneOrDefault();
    }

    public void ReturnMainMenu()
    {
        optionsMenu.SetActive(false);

        continueMenu.SetActive(false);
        creditsMenu.SetActive(false);
        levelSelectMenu.SetActive(false);
        mainMenu.SetActive(true);

        foreach (GameObject optionMenuButton in optionMenuButtons)
        {
            optionMenuButton.SetActive(false);
        }
    }

}
