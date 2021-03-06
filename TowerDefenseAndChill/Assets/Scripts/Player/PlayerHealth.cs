﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth: MonoBehaviour
{
    public int startingHealth = 100;
	public int startingGold = 20;
	public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public int currentHealth;
	public int currentGold;

   BoxCollider endCollider;
	
	
    Animator anim;
    AudioSource playerAudio;
   
  
    bool isDead;
    bool damaged;


    void Awake ()
    {
		anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        currentHealth = startingHealth;
		  currentGold = startingGold;
	}

   void OnTriggerEnter(Collider other) {
      Destroy(other.gameObject);
      TakeDamage (10);
   }


    void Update ()
    {
        
    }
 
	public void TakeGold (int amount)
    {
		currentGold += amount;
	}

   public void RemoveGold (int amount)
   {
      currentGold -= amount;
   }

    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;
		   healthSlider.value = currentHealth;

        playerAudio.Play ();
		
		if(currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;
		
		anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play ();

		
		Destroy(gameObject);
    }


    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }
}
