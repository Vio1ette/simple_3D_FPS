using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Awake()
    {
        Messenger<int>.AddListener(GameEvent.PLAYER_HIT,OnPlayerHit);
    }

    private void OnDestroy()
    {
        Messenger<int>.RemoveListener(GameEvent.PLAYER_HIT,OnPlayerHit);
    }

    private void Start()
    {
        health = 3;
        numOfHearts = 3;
    }

    private void Update()
    {

        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i<health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else hearts[i].enabled = false;
        }
    }

    public void OnPlayerHit(int _health)
    {
        health = _health;
    }

}
