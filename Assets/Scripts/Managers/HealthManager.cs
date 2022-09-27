using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite _fullHeart;
    [SerializeField] private Sprite _emptyHeart;

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
