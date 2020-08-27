using System;
using System.Collections;
using UnityEngine;

public class ShipEnemy : Enemy {

	[Serializable]
	private enum AIState {
		SEEKING,
		ATTACKING
	}

	[SerializeField] private AIState state = AIState.SEEKING;

	
	[SerializeField] private float speed = 1.5f;
	[SerializeField] protected float bulletSpeed = 2f;
	[SerializeField] private float delayBetweenShots = 1f;
	[SerializeField] private float lookTime = 2f;
	[SerializeField] protected bool canShoot = true;
	[SerializeField] protected GameObject bulletPrefab;
	[SerializeField] protected Transform cannon;

	[Tooltip("This is how far away the ship will attempt to get to the player when they start to shoot.")]
	[SerializeField] private float seekDistance = 5f;

	[Tooltip("This is the distance away from the player that when engaged the ship will disengage and attempt to seek the player again.")]
	[SerializeField] private float loseDistance = 7f;

	protected override void Tick() {
		base.Tick();

		float distToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);

		if (state == AIState.SEEKING) {
			if (distToPlayer <= seekDistance)
				state = AIState.ATTACKING;
		} else if (state == AIState.ATTACKING) {
			if (distToPlayer >= loseDistance)
				state = AIState.SEEKING;
		}
		
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.forward, Player.Instance.transform.position - transform.parent.position), lookTime * Time.deltaTime);

		animator.SetBool("Shooting", false);
		animator.ResetTrigger("Damaged");
		
		Think();
	}

	//handle thinking logic
	public void Think() {
		if (state == AIState.SEEKING) {
			SeekPlayer();
		} else if (state == AIState.ATTACKING) {
			AttackPlayer();
		}
	}

	public void SeekPlayer() {
		// move directly in the direction of the player at the given speed
		Vector3 dir = (Player.Instance.transform.position - transform.parent.position).normalized * speed;
		transform.parent.position = Vector3.Lerp(transform.parent.position, transform.parent.position + dir, Time.deltaTime);
		// transform.parent.Translate((Player.Instance.transform.position - transform.parent.position).normalized * speed);
	}

	public void AttackPlayer() {
		Shoot();
	}
	
	protected virtual void Shoot()
	{
		if (!canShoot)
			return;
		
		animator.SetBool("Shooting", true);

		StartCoroutine(BulletDelay());
		Quaternion rot = transform.rotation;
		GameObject b = Instantiate(bulletPrefab, cannon.position, rot, null);
		b.transform.SetParent(null);

		EnemyBulletManager.Instance.DoBulletTask(b, bulletSpeed);
	}

	protected IEnumerator BulletDelay()
	{
		canShoot = false;

		float elapsed = 0f;
		while (elapsed <= delayBetweenShots)
		{
			elapsed += Time.deltaTime;
			yield return null;
		}

		canShoot = true;
	}
	
	private void OnTriggerEnter2D(Collider2D collision) {
		if (!collision.CompareTag("Player")) return;
		if (Health <= 0) return;
		Player.Instance.Damage(1);
	}
	
	public override void Damage(int damage)
	{
		base.Damage(damage);
		
		animator.SetTrigger("Damaged");

		if (Health <= 0)
			Kill();
	}
}