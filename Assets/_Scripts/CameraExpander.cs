using UnityEngine;

public class CameraExpander : MonoBehaviour
{

	public Transform playerTransform;

	public float trailTime = 1.0f;
	public float maxDistanceFromOrigin = 5.0f;

	private Camera camera;

	private void Start()
    {
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		camera = GetComponent<Camera>();
    }

	/* Let camera size = z, and distance away from the origin be dx.
	 
		 z = dx / 2 + 5
		 */

	private void Update()
    {
	    Vector3 position = transform.position;
	    Vector3 playerPos = playerTransform.position;
	    Vector3 midPoint = new Vector3((position.x + playerPos.x) / 2, (position.y + playerPos.y) / 2, -1f);
		float distance = Vector3.Distance(Vector3.zero, position);
		position = Vector3.Lerp(position, midPoint, trailTime * Time.deltaTime);
		transform.position = position;
		Vector3 pos = position;
		if (transform.position.x > maxDistanceFromOrigin)
			pos.x = maxDistanceFromOrigin;
		else if (-transform.position.x > maxDistanceFromOrigin)
			pos.x = -maxDistanceFromOrigin;

		if (transform.position.y > maxDistanceFromOrigin)
			pos.y = maxDistanceFromOrigin;
		else if (-transform.position.y > maxDistanceFromOrigin)
			pos.y = -maxDistanceFromOrigin;

		transform.position = pos;
	}
}
