using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

	public Slider m_Slider;                             // The slider to represent how much health the tank currently has.
	public Color m_FullHealthColor = Color.green;       // The color the health bar will be when on full health.
	public Color m_ZeroHealthColor = Color.red;         // The color the health bar will be when on no health.
	public Image m_FillImage;                           // The image component of the slider.
    public AudioClip deathClip;
	public int startingHealth;
	public int currentHealth;
	// public PlayerStatus player;

    Animator anim;
    AudioSource enemyAudio;
    CapsuleCollider capsuleCollider;

	public bool isDead;

	void Awake ()
	{
		currentHealth = startingHealth;

		anim = GetComponent <Animator> ();
		enemyAudio = GetComponent <AudioSource> ();
		capsuleCollider = GetComponent <CapsuleCollider> ();

	}

    void StartSinking()
    {
        //Destroy(gameObject, 2f);
    }

	void Update ()
	{

	}


	private void OnEnable()
	{
		// Update the health slider's value and color.
		SetHealthUI();
	}


	public void TakeDamage (int amount)
	{
		if(isDead)
			return;
		enemyAudio.Play ();

		currentHealth -= amount;
		// player.TakeGold(5);

		// Change the UI elements appropriately.
		SetHealthUI ();

		if(currentHealth <= 0)
		{
			// player.TakeGold(10);
			Death ();
		}


	}


	void Death ()
	{
		isDead = true;

		capsuleCollider.isTrigger = true;

		anim.SetTrigger ("Dead");

		enemyAudio.clip = deathClip;
		enemyAudio.Play ();

		 // Destroy(gameObject);

	}

	private void SetHealthUI ()
	{
		// Set the slider's value appropriately.
		m_Slider.value = currentHealth;
		// Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
		m_FillImage.color = Color.Lerp (m_ZeroHealthColor, m_FullHealthColor, currentHealth / (1.0f*startingHealth) );
	}

}
