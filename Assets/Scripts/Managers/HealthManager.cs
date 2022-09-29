using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] public static int health = 3;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite _fullHeart;
    [SerializeField] private Sprite _emptyHeart;

    PlayerScript _player;

    /// <summary>
    /// we nedd this method to avoid problrm with zero hearts when we restart game
    /// that happens because health is static
    /// </summary>
    private void Awake()
    {
        health = 3;
    }

    void Update()
    {
        foreach (Image heart in hearts)
        {
            heart.sprite = _emptyHeart;
        }
        for (int i = 0; i < health; i++)
        {
            hearts[i].sprite = _fullHeart;
        }
    }
}
