using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _characters;
    public static GameManager Instance;

    private int _charIndex;
    public int CharIndex
    {
        get { return _charIndex; }
        set { _charIndex = value; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;//create copy if it doesn't exist
            DontDestroyOnLoad(gameObject);//game manager won't be destroyed and come with us to the new scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == "GamePlay")
        {
            Instantiate(_characters[_charIndex]);
        }
    }
}
