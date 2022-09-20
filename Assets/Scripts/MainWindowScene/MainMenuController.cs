using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SelectCharacter");
    }

    private void ContinueGame()
    {
        //TODO - load last save
    }

    private void OpenGameSettings()
    {
        //TODO - code to open another canvas
    }

    private void CloseTheGame()
    {
        //TODO - code to close the game
    }
}
