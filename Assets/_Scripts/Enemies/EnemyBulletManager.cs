using System;
using System.Collections;
using UnityEngine;

public class EnemyBulletManager : MonoBehaviour {
	
	public static EnemyBulletManager Instance { get; private set; }

	private void Awake() {
		if (Instance != null) {
			Destroy(gameObject);
			return;
		}

		Instance = this;
	}

	public void DoBulletTask(GameObject bullet, float speed) {
		StartCoroutine(ApplyBulletForce(bullet, speed));
	}

	private IEnumerator ApplyBulletForce(GameObject bullet, float speed)
	{
		while (bullet != null && Vector3.Distance(Vector3.zero, bullet.transform.position) < 20f)
		{
			bullet.transform.Translate(new Vector3(0, Time.deltaTime * speed, 0));
			yield return null;
		}

		if (bullet != null)
		{
			Destroy(bullet);
		}
	}
	
}