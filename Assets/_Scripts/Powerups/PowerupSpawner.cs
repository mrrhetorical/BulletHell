using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerupSpawner : MonoBehaviour
{
	public static PowerupSpawner Instance { get; private set; }

	[SerializeField] GameObject[] _spawnablePrefabs;

	[SerializeField] private float _despawnTime = 20f;
	[SerializeField] private float _timeBetweenSpawns = 60f;
	[SerializeField] private float _maxFluxBetweenSpawns = 30f;

	private void Start()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;

		StartCoroutine(SpawnPowerups(_timeBetweenSpawns, _maxFluxBetweenSpawns));
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

	public void SpawnPowerup(Vector3 position)
	{
		int index = Random.Range(0, _spawnablePrefabs.Length - 1);
		GameObject spawned = Instantiate(_spawnablePrefabs[index], position, Quaternion.identity, null);
		spawned.GetComponent<IPowerup>().Despawn(_despawnTime);
	}
}
