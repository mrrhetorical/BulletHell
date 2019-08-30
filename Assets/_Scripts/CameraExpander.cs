using UnityEngine;

public class CameraExpander : MonoBehaviour
{

	public Transform playerTransform;

	public float trailTime = 1.0f;
	public float maxDistanceFromOrigin = 5.0f;

	private Camera camera;

    void Start()
    {
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		camera = GetComponent<Camera>();
    }

	/* Let camera size = z, and distance away from the origin be dx.
	 
		 z = dx / 2 + 5
		 */

    void Update()
    {
		Vector3 midPoint = new Vector3((transform.position.x + playerTransform.position.x) / 2, (transform.position.y + playerTransform.position.y) / 2, -1f);
		float distance = Vector3.Distance(Vector3.zero, transform.position);
		transform.position = Vector3.Lerp(transform.position, midPoint, trailTime * Time.deltaTime);
		Vector3 pos = transform.position;
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
