using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Damageable
{

	protected Animator animator;

	private static readonly int Destroyed = Animator.StringToHash("Destroyed");

	[SerializeField] protected int defaultHealth = 1;
	
	private void Start()
	{
		animator = GetComponent<Animator>();
		Health = defaultHealth;
		Enable();
	}

	public virtual void Enable() {}

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

	public override void Damage(int damage)
	{
		base.Damage(damage);

		if (Health <= 0)
			Kill();
	}
}
