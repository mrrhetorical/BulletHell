using UnityEngine;

public class Damageable : MonoBehaviour {

	[SerializeField] public int Health { get; set; }

	public virtual void Damage(int amount) {
		Health -= amount;
	}

}