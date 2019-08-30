﻿using UnityEngine;

public class Bullet : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			collision.gameObject.GetComponent<Enemy>().Damage(1);
			Destroy(gameObject);
		}
	}
}
