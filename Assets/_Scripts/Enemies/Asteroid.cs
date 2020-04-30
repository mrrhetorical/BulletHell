using UnityEngine;

public class Asteroid : Enemy
{

	public float spin;
	public float asteroidSpeed = 1.0f;

	public bool spawnRot;

	protected override void Enable()
	{

		Vector3 deltaPos = Player.Instance.transform.position - transform.parent.position;

		if (spawnRot)
		{
			Quaternion desired = Quaternion.LookRotation(Vector3.forward, deltaPos);
			Vector3 dEuler = desired.eulerAngles;
			float angle = Player.Instance.AsteroidAngle / 2f;
			dEuler.z += Random.Range(-angle, angle);

			transform.parent.rotation = Quaternion.Euler(dEuler);
		}

		asteroidSpeed = Random.Range(0.5f, 3f);

		animator = GetComponentInChildren<Animator>();
		spin = Random.Range(-50f, 50f);
	}

	protected override void Tick()
	{
		base.Tick();

		if (spawnRot)
		{
			Transform t = transform;
			Quaternion rotation = t.rotation;
			Vector3 rot = rotation.eulerAngles;
			rot.z += spin;
			transform.rotation = Quaternion.Lerp(rotation, Quaternion.Euler(rot), Time.deltaTime);
		}

		transform.parent.Translate(asteroidSpeed * Time.deltaTime * Vector3.up);

		if (Vector3.Distance(Vector3.zero, transform.parent.position) > 20f)
		{
			Destroy();
		}
	}

	protected override void Destroy()
	{
		base.Destroy();
		Destroy(transform.parent.gameObject);
	}

	public void SetRotation(float rotation)
	{
		transform.parent.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotation));
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag("Player")) return;
		if (health <= 0) return;
		Player.Instance.Damage(1);
		Destroy();
	}
}