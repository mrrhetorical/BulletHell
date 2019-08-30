using System.Collections;
using UnityEngine;
using TMPro;

public class ShootingController : MonoBehaviour
{

	public Animator heroAnimator;

	public Transform leftCannon;
	public Transform rightCannon;

	public GameObject bulletPrefab;

	public TextMeshProUGUI ammoField;

	public float bulletSpeed = 8f;

	[SerializeField] private float delayBetweenShots = 0.0625f;
	private bool canShoot = true;

	public long ammoCount = 50;
	public long maxAmmoCount = 200;

	void Start()
	{
		heroAnimator = GetComponent<Animator>();
	}

	void Update()
	{
		if (Input.GetButtonDown("Fire1") && canShoot && ammoCount > 0 && !GetComponent<MovementController>().IsBoosting)
		{
			heroAnimator.SetBool("IsShooting", true);
		}

		if (Input.GetButton("Fire1"))
		{
			Shoot();
			if (ammoCount <= 0)
				heroAnimator.SetBool("IsShooting", false);
		}

		if (Input.GetButtonUp("Fire1"))
		{
			heroAnimator.SetBool("IsShooting", false);
		}
	}

	private void FixedUpdate()
	{
		ammoField.text = ammoCount.ToString();
	}

	void Shoot()
	{
		if (!canShoot)
			return;

		if (!(ammoCount > 0))
			return;

		if (ammoCount > maxAmmoCount)
			ammoCount = maxAmmoCount;

		ammoCount--;

		StartCoroutine(BulletDelay());
		var left = Instantiate(bulletPrefab, leftCannon.position, transform.rotation, null);
		var right = Instantiate(bulletPrefab, rightCannon.position, transform.rotation, null);

		StartCoroutine(ApplyBulletForce(left));
		StartCoroutine(ApplyBulletForce(right));
	}

	private IEnumerator ApplyBulletForce(GameObject bullet)
	{

		while (bullet != null && Vector3.Distance(Vector3.zero, bullet.transform.position) < 20f)
		{
			bullet.transform.Translate(new Vector3(0, Time.deltaTime * bulletSpeed, 0));
			yield return null;
		}

		if (bullet != null)
		{
			Destroy(bullet);
		}
	}

	private IEnumerator BulletDelay()
	{
		canShoot = false;

		float elapsed = 0f;
		while (elapsed <= delayBetweenShots)
		{
			elapsed += Time.deltaTime;
			yield return null;
		}

		canShoot = true;
	}
}
