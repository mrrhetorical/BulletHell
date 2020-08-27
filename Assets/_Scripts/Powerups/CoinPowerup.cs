using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPowerup : MonoBehaviour, IPowerup
{
	public void Pickup()
	{
		throw new System.NotImplementedException();
	}

	public void Despawn(float a) {
		StartCoroutine(DespawnTask(a));
	}

	public IEnumerator DespawnTask(float a)
	{
		throw new System.NotImplementedException();
	}
}
