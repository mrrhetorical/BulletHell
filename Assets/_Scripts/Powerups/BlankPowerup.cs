using System.Collections;
using UnityEngine;

public class BlankPowerup : MonoBehaviour, IPowerup {
	public void Pickup() {
		Player.Instance.Blanks++;
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

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			Pickup();
		}
	}
}