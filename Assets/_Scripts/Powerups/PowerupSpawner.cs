using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class PowerupSpawner : MonoBehaviour
{
	public static PowerupSpawner Instance { get; private set; }

	[SerializeField] private GameObject[] spawnablePrefabs;

	[SerializeField] private float despawnTime = 20f;
	[SerializeField] private float timeBetweenSpawns = 60f;
	[SerializeField] private float maximumDistanceDelta = 30f;

	private void Start()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;

		StartCoroutine(SpawnPowerups(timeBetweenSpawns, maximumDistanceDelta));
	}

	private IEnumerator SpawnPowerups(float timeBetweenDrops, float maxDistance)
	{
		while (true)
		{
			float timeToWait = timeBetweenDrops + (Random.Range(0, 2) == 1 ? 1 : -0.5f * Random.Range(0f, maxDistance));
			float elapsed = 0f;
			while (elapsed < timeBetweenDrops)
			{
				elapsed += Time.deltaTime;
				yield return null;
			}

			SpawnPowerup();

		}
	}

	public void SpawnPowerup()
	{
		Vector3 spawnPos = new Vector3(Random.Range(-11f, 11f), Random.Range(-9f, 9f), 0f);
		SpawnPowerup(spawnPos);
	}

	private void SpawnPowerup(Vector3 position)
	{
		if (spawnablePrefabs.Length == 0)
			return;
		
		int index = Random.Range(0, spawnablePrefabs.Length - 1);
		GameObject spawned = Instantiate(spawnablePrefabs[index], position, Quaternion.identity, null);
		spawned.GetComponent<IPowerup>().Despawn(despawnTime);
	}
}
