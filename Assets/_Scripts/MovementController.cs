using UnityEngine;

public class MovementController : MonoBehaviour
{

	public static MovementController Instance { get; private set; }

	public new Camera camera;

	public float playerSpeed = 1.0f;
	public float cursorFloatSpeed = 10.0f;
	public float movementFloatSpeed = 2.0f;

	public Vector2 screenBounds = new Vector2(12f, 8f);

	[SerializeField] private float _boostFuel = 10f;
	[SerializeField] private float _boostModifier = 2f;
	
	private static readonly int Boosting = Animator.StringToHash("IsBoosting");

	public bool IsBoosting { get; private set; }


	private void Start() {
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		camera = FindObjectOfType<Camera>();
	}

	public void Update() {

		Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);

		Transform tForm = transform;
		transform.rotation = Quaternion.Lerp(tForm.rotation, Quaternion.LookRotation(Vector3.forward, mousePos - tForm.position), Time.deltaTime * cursorFloatSpeed);

		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f) * playerSpeed;

		if (Input.GetButton("Boost") && _boostFuel > 0) {
			movement *= _boostModifier;
			IsBoosting = true;
			Player.Instance.animator.SetBool(Boosting, true);
			_boostFuel = _boostFuel - Time.deltaTime > 0 ? _boostFuel - Time.deltaTime : 0;
		} else if (Input.GetButtonUp("Boost")) {
			IsBoosting = false;
			Player.Instance.animator.SetBool(Boosting, false);
		}

		Vector3 position = transform.position;
		position = Vector3.Lerp(position, position + movement, Time.deltaTime * movementFloatSpeed);
		transform.position = position;
		Vector3 pos = position;
		if (pos.x > screenBounds.x)
			pos.x = screenBounds.x;
		else if (-pos.x > screenBounds.x)
			pos.x = -screenBounds.x;

		if (pos.y > screenBounds.y)
			pos.y = screenBounds.y;
		else if (-pos.y > screenBounds.y)
			pos.y = -screenBounds.y;

		transform.position = pos;
	}
}