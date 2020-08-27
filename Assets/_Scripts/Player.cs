using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : Damageable
{

	public static Player Instance { get; private set; }

	public Animator animator;

	[SerializeField] private int startingHealth = 3;
	[SerializeField] private int startingBlanks = 3;
	
	[SerializeField] private int maxBlanks = 3;
	[SerializeField] private int maxHealth = 3;
	
	[SerializeField] public int Score { get; private set; }
	
	[SerializeField] public int Blanks { get; set; }

	[SerializeField] public float AsteroidAngle = 60f;

	public bool IsDead = false;

	[SerializeField] private bool invincible = false;

	[SerializeField] private TextMeshProUGUI _scoreText;
	[SerializeField] private TextMeshProUGUI _healthText;

	public ShootingController shootingController { get; private set; }

	private void Start()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;

		if (AsteroidAngle <= 0)
		{
			Debug.LogWarning("Invalid asteroid Angle! (Must be greater than zero!)");
			AsteroidAngle = 60f;
		}

		shootingController = GetComponent<ShootingController>();
		
		Health = startingHealth;
		Blanks = startingBlanks;
		
		Score = 0;

		animator = GetComponent<Animator>();

		StartCoroutine(UpdateScore());
	}

	private void FixedUpdate()
	{
		string text = Score.ToString();
		if (Score < 10)
			text = "0" + text;

		_scoreText.text = text;

		if (Health > maxHealth)
			Health = maxHealth;

		if (Blanks > maxBlanks)
			Blanks = maxBlanks;

		_healthText.text = $"HP: {Health}\nBlanks: {Blanks}";
	}

	private IEnumerator UpdateScore()
	{
		while (!IsDead)
		{
			yield return new WaitForSeconds(1f);
			Score++;
		}
	}

	public override void Damage(int damage)
	{
		if (invincible)
			return;

		Health -= damage;
		if (Health <= 0)
			Kill();
		else
			animator.SetBool("IsDamaged", true);
	}

	private void Kill()
	{
		IsDead = true;
		animator.SetBool("IsDead", true);
	}

	public void EnterInvincibility()
	{
		invincible = true;
	}

	public void ExitInvincibility()
	{
		invincible = false;
		animator.SetBool("IsDamaged", false);
	}

	public void UseBlank() {
		if (Blanks <= 0)
			return;
		
		Blanks--;
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
		{
			Destroy(obj);
		}
	}

	public void EndGame() {
		MenuHandler.Instance.EnableGameOverMenu();
		MouseController.GetInstance().DisableCursorOverlay();
		PauseTime();
	}

	private void PauseTime() {
		animator.enabled = false;
		GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<MovementController>().enabled = false;
		GetComponent<ShootingController>().enabled = false;

		Time.timeScale = 0.05f;
	}
}
