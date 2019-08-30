using System.Collections;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
	[SerializeField] private Vector2 screenInnerBounds = new Vector2(12, 8);
	[SerializeField] private Vector2 screenOuterBounds = new Vector2(14, 10);

	public GameObject asteroidPrefab;

	public float spawnDelay = 0.5f;
	private float timeSinceLastSpawn = 0f;

	private void Start()
	{
		
	}

	private void Update()
	{
		if (timeSinceLastSpawn < spawnDelay)
			timeSinceLastSpawn += Time.deltaTime;
		else
		{
			timeSinceLastSpawn = 0f;
			bool high, far;
			high = Random.Range(0, 2) == 1;
			far = Random.Range(0, 2) == 1;
			Vector3 spawnPos = new Vector3(Random.Range(far ? 14 : -16, far ? 16 : -14), Random.Range(high ? 10 : -12, high ? 12 : -10), 0f) ;
			Instantiate(asteroidPrefab, spawnPos, Quaternion.identity, transform);
		}
	}
}
