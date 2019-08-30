using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DynamicBackground : MonoBehaviour
{

	[SerializeField] private GameObject _starPrefab;
	[SerializeField] private float _minDistanceBetweenStars = 0.1f;
	[SerializeField] private int _maxStars;

	private List<GameObject> _spawnedStars = new List<GameObject>();

	private void Start()
	{
		GenerateStars();
	}

	private void GenerateStars()
	{
		for (int i = 0; i < _maxStars; i++)
		{


			Vector3 position;

			do
			{
				position = new Vector3(Random.Range(-12f, 12f), Random.Range(-10f, 10f), 1f);
			} while (!IsFarEnoughAway(position));


			Instantiate(_starPrefab, position, Quaternion.identity, transform);
		}
	}

	private bool IsFarEnoughAway(Vector3 pos)
	{
		foreach (var obj in _spawnedStars)
		{
			if (Vector3.Distance(obj.transform.position, pos) < _minDistanceBetweenStars)
				return false;
		}

		return true;
	}
}
