using UnityEngine;
using System.Collections;

public class AmmoPowerup : MonoBehaviour, IPowerup
{

	public void Pickup()
	{
		// Player.Instance.GetComponent<ShootingController>().ammoCount += 50;
		//todo: rework into temporary quicker fire rate
		Destroy(gameObject);
	}

	public IEnumerator Despawn(float timeToDespawn)
	{
		float elapsed = 0f;
		while (elapsed < timeToDespawn)
		{
			elapsed += Time.deltaTime;
			yield return null;
		}

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