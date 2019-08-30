﻿using UnityEngine;

public class Asteroid : Enemy
{

	public float spin;
	public float asteroidSpeed = 1.0f;

	protected override void Enable()
	{

		Vector3 deltaPos = Player.Instance.transform.position - transform.parent.position;

		Quaternion desired = Quaternion.LookRotation(Vector3.forward, deltaPos);
		var dEuler = desired.eulerAngles;
		float angle = Player.Instance.AsteroidAngle / 2f;
		dEuler.z += Random.Range(-angle, angle);

		transform.parent.rotation = Quaternion.Euler(dEuler);

		asteroidSpeed = Random.Range(0.5f, 3f);

		animator = GetComponentInChildren<Animator>();
		spin = Random.Range(-50f, 50f);
	}

	protected override void Tick()
	{
		base.Tick();
		Vector3 rot = transform.rotation.eulerAngles;
		rot.z += spin;
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rot), Time.deltaTime);

		transform.parent.Translate(Vector3.up * asteroidSpeed * Time.deltaTime);

		if (Vector3.Distance(Vector3.zero, transform.parent.position) > 20f)
		{
			Destroy();
		}
	}

	public override void Destroy()
	{
		base.Destroy();
		Destroy(transform.parent.gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			Player.Instance.Damage(1);
			Destroy();
		}
	}
}