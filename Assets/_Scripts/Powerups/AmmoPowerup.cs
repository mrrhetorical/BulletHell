using UnityEngine;
using System.Collections;

public class AmmoPowerup : MonoBehaviour, IPowerup
{
	public void Pickup()
	{
		GameObject hidden = Instantiate(new GameObject());
		hidden.AddComponent(typeof(AmmoPowerup));
		hidden.name = "Ammo Task";

		hidden.GetComponent<AmmoPowerup>().StartCoroutine(hidden.GetComponent<AmmoPowerup>().StartPowerupTask());
		
		Destroy(gameObject);
	}

	public void Despawn(float time) {
		StartCoroutine(DespawnTask(time));
	}

	public IEnumerator DespawnTask(float timeToDespawn)
	{
		float elapsed = 0f;
		while (elapsed < timeToDespawn)
		{
			elapsed += Time.deltaTime;
			yield return null;
		}
		
		if (gameObject)
			Destroy(gameObject);
	}

	private IEnumerator StartPowerupTask()
	{
		ShootingController sController = Player.Instance.shootingController;
		ShootingSpeed original = sController.ShootSpeed;
		sController.DelayBetweenShots = ShootingSpeed.FASTEST.Speed;

		float t = 10;
		while (t > 0)
		{
			t -= Time.deltaTime;
			yield return null;
		}

		sController.DelayBetweenShots = original.Speed;

		if (gameObject)
			Destroy(gameObject);
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			Pickup();
		}
	}
}