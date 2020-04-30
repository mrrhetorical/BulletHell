using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

	protected Animator animator;

	protected int health = 1;
	private static readonly int Destroyed = Animator.StringToHash("Destroyed");

	private void Start()
	{
		animator = GetComponent<Animator>();
		Enable();
	}

	protected virtual void Enable() {}

	private void Update()
	{
		Tick();
	}

	protected virtual void Tick() {}

	protected virtual void Kill()
	{
		animator.SetBool(Destroyed, true);
	}

	protected virtual void Destroy()
	{
		if (Random.Range(0f, 1f) <= 0.02)
		{
			PowerupSpawner.Instance.SpawnPowerup();
		}

		Destroy(gameObject);
	}

	public virtual void Damage(int damage)
	{
		health -= damage;

		if (health <= 0)
			Kill();
	}
}
