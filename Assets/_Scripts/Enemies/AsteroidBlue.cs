using UnityEngine;

public class AsteroidBlue : Asteroid
{
	protected override void Destroy()
	{
		const float distanceFixed = 0.2f;
		for (int i = 0, t = 0; i < 12; i++, t += 30)
		{
			Vector3 position = transform.position;
			
			Vector3 offset = new Vector3(distanceFixed * Mathf.Cos(Mathf.Deg2Rad * t), distanceFixed * Mathf.Sin(Mathf.Deg2Rad * t));
			GameObject child = Instantiate(AsteroidSpawner.GetInstance().asteroidBabyPrefab, position + offset, Quaternion.identity);
			
			child.transform.rotation = Quaternion.LookRotation(Vector3.forward, child.transform.position - position);

			base.Destroy();
			Destroy(transform.parent.gameObject);
		}
	}
}