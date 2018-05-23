using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vanisher;

public class PlayerHealth : MonoBehaviour {
    public int playerHealth;
    public int maxHealth;
	public Image[] hearts;
	private int heartNum = 2;
	private int healthPerHeart;

    public void setHealth(int health)
    {
        if (health <= 0)
        {
            playerHealth = 0;
            GameManager.GameOver();
            //Debug.Log("Game Over");
            return;
        }
        if (health < maxHealth)
            playerHealth = health;
        else
            playerHealth = maxHealth;
//		Debug.Log ("enter setHealth");
    }

    public void restore(int restoredHealth)
    {
        setHealth(playerHealth + restoredHealth);
    }
    public void damage(int damageHealth)
    {
        setHealth(playerHealth - damageHealth);
		healthPerHeart = maxHealth / heartNum;
    }

    private void Update()
    {
		if (healthPerHeart > 0){
			int heart = playerHealth / healthPerHeart;
			int heartFill = playerHealth % healthPerHeart;
			for (int i = 0; i < heart; i++) {
				hearts [i].fillAmount = 1;
			}
			hearts[heart].fillAmount = heartFill / (float)healthPerHeart;
			for (int j = heart+1; j < hearts.Length; j++) {
				hearts [j].fillAmount = 0;
			}
//			if (playerHealth % healthPerHeart == 0) {
//
//				if (heart == hearts.Length) {
//					hearts [heart - 1].fillAmount = 1;
//					return;
//				}
//				if (heart > 0) {
//					hearts [heart].fillAmount = 0;
//					hearts [heart - 1].fillAmount = 1;
//				} else {
//					hearts [heart].fillAmount = 0;
//				}
//				return;
//			}
            Debug.Log("heart= " + heart);
			Debug.Log("health = " + playerHealth);
		}
    }
}
