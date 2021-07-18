using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeartManager : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfFullHeart;
    public Sprite emptyHeart;
    public FloatValue heartContainers;
    public FloatValue playerCurrentHealth;
    private const int MaxHealth = 5;

    // Start is called before the first frame update
    void Start()
    {
        InitHearts();
    }

    public void InitHearts()
    {
        for(int i = 0; i < heartContainers.RuntimeValue; i++)
        //for (int i = 0; i < 5; i++)
        {
            if (i < hearts.Length)
            {
                hearts[i].gameObject.SetActive(true);
                hearts[i].sprite = fullHeart;
            }
        }
    }
    public void UpdateHearts()
    {
        InitHearts();
        float tempHealth = playerCurrentHealth.RuntimeValue / 2;
        for(int i = 0; i < heartContainers.RuntimeValue; i++)
        {
            if (i < hearts.Length)
            {
                if (i <= tempHealth - 1)
                {
                    //Full Health
                    hearts[i].sprite = fullHeart;
                }
                else if (i >= tempHealth)
                {
                    //Empty Health
                    hearts[i].sprite = emptyHeart;
                }
                else
                {
                    //half full heart
                    hearts[i].sprite = halfFullHeart;
                }
            }
        }
    }
}
