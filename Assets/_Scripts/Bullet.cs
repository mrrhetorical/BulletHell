using System;
using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour {

	[Tooltip("Valid types: BossEnemy, Enemy, Player")] [SerializeField]
	private string[] killTags;
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (killTags.Contains(collision.tag))
		{
			Damageable damageable = collision.gameObject.GetComponent<Damageable>();
			if (damageable != null) {
				damageable.Damage(1);
				
				Destroy(gameObject);
			}
		}
	}
}
