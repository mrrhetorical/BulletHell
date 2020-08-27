using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class BossEnemy : ShipEnemy {
	
	public static BossEnemy Instance { get; private set; }

	[SerializeField] private float timeBetweenSpecials = 0f;
	private bool canShootSpecial = true;
	[SerializeField] private float omniDirectionalBulletSpeed = 10f;
	[SerializeField] private float sprayBulletSpeed = 10f;
	
	public override void Enable() {
		base.Enable();

		if (Instance != null) {
			Destroy(gameObject);
			return;
		}

		Instance = this;
	}

	public new void Destroy() {
		Instance = null;
		base.Destroy();
	}

	protected override void Shoot() {
		int attackType = Random.Range(0, 2);
		
		if (attackType == 0) {
			NormalAttack();
		}
		else {
			int style = Random.Range(0, 2);
			if (style == 0) {
				SpecialOmniDirectional();
			} else if (style == 1) {
				SpecialSpray();
			}
		}

	}

	public void NormalAttack() {
		if (!canShoot)
			return;

		animator.SetBool("Shooting", true);

		StartCoroutine(BulletDelay());
		Quaternion rot = transform.rotation;
		GameObject b = Instantiate(bulletPrefab, cannon.position, rot, null);
		b.transform.SetParent(null);

		EnemyBulletManager.Instance.DoBulletTask(b, bulletSpeed);
	}

	public void SpecialOmniDirectional() {
		if (!canShootSpecial)
			return;
		
		
		animator.SetBool("Shooting", true);

		StartCoroutine(SpecialBulletDelay());

		for (int i = 0, t = 0; i < 24; i++, t+= 15) {
			Vector3 offset = new Vector3(0.1f * Mathf.Cos( Mathf.Deg2Rad * t),0.1f * Mathf.Sin(Mathf.Deg2Rad * t));
			GameObject b = Instantiate(bulletPrefab, transform.position + offset, transform.rotation, null);
			b.transform.SetParent(null);
			b.transform.rotation = Quaternion.LookRotation(Vector3.forward, b.transform.position - transform.position);
			EnemyBulletManager.Instance.DoBulletTask(b, omniDirectionalBulletSpeed);
		}
	}

	public void SpecialSpray() {
		if (!canShootSpecial)
			return;
		
		animator.SetBool("Shooting", true);

		StartCoroutine(SpecialBulletDelay());

		Debug.Log("Spray attack");
		
		for (int i = 0, t = (int) transform.rotation.eulerAngles.z + 60; i < 13; i++, t+= 5) {
			Vector3 offset = new Vector3(0.1f * Mathf.Cos( Mathf.Deg2Rad * t),0.1f * Mathf.Sin(Mathf.Deg2Rad * t));
			GameObject b = Instantiate(bulletPrefab, transform.position + offset, transform.rotation, null);
			b.transform.SetParent(null);
			b.transform.rotation = Quaternion.LookRotation(Vector3.forward, b.transform.position - transform.position);
			EnemyBulletManager.Instance.DoBulletTask(b, sprayBulletSpeed);
		}
	}
	
	protected IEnumerator SpecialBulletDelay()
	{
		canShootSpecial = false;

		float elapsed = 0f;
		while (elapsed <= timeBetweenSpecials)
		{
			elapsed += Time.deltaTime;
			yield return null;
		}

		canShootSpecial = true;
	}

	//todo: implement a health bar for boss enemy
}