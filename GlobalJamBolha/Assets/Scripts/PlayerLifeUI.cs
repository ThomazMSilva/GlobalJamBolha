using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;

public class PlayerLifeUI : MonoBehaviour
{
    [SerializeField] private PlayerLife playerLife;
    [SerializeField] private Image[] bubbleImage;
    [SerializeField] private Sprite fullBubble;
    [SerializeField] private Sprite burstBubble;

    private int health;

    void Start()
    {
        UpdateHUD(this, 0, playerLife.CurrentHealth);

        playerLife.OnHealthChanged += UpdateHUD;
    }

    private void UpdateHUD (object sender, int oldHealth, int newHealth)
    {
        health = newHealth;

        for (int i = 0; i < bubbleImage.Length; i++)
        {
            if (i >= health) 
            {
                bubbleImage[i].sprite = burstBubble;
            }

            else bubbleImage[i].sprite = fullBubble;
        }
    }

}
