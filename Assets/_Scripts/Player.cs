using UnityEngine;
using System.Collections;
using TMPro;

public class Player : MonoBehaviour
{

	public static Player Instance { get; private set; }

	public Animator animator;

	[SerializeField] private int Health { get; set; }

	[SerializeField] private int Score { get; set; }

	[SerializeField] public float AsteroidAngle = 60f;

	public bool IsDead = false;

	[SerializeField] private bool invincible = false;

	[SerializeField] private TextMeshProUGUI _scoreText;

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

		Health = 2;
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
	}

	private IEnumerator UpdateScore()
	{
		while (!IsDead)
		{
			yield return new WaitForSeconds(1f);
			Score++;
		}
	}

	public void Damage(int damage)
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

	public void PauseTime()
	{

		animator.enabled = false;
		GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<MovementController>().enabled = false;
		GetComponent<ShootingController>().enabled = false;

		Time.timeScale = 0.05f;
	}
}
