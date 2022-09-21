using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SelectPlayerController : MonoBehaviour
{
    public void PlayGame()
    {
        int selectrdPlayer = int.Parse(EventSystem.current.currentSelectedGameObject.name);

        GameManager.Instance.CharIndex = selectrdPlayer;

        SceneManager.LoadScene("GamePlay");
    }


}
