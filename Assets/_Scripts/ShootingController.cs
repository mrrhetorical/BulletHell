using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class ShootingController : MonoBehaviour
{

	public Animator heroAnimator;

	public Transform leftCannon;
	public Transform rightCannon;

	public GameObject bulletPrefab;
	
	public float bulletSpeed = 8f;

	public ShootingSpeed ShootSpeed { get; set; }
	
	[SerializeField] public float DelayBetweenShots = 0.0625f;
	private bool canShoot = true;
	private static readonly int IsShooting = Animator.StringToHash("IsShooting");

	private void Start()
	{
		heroAnimator = GetComponent<Animator>();
		string sSpeed = PlayerPrefs.GetString("ShootingSpeed");

		ShootingSpeed spd = null;
		if (sSpeed != null)
			spd = ShootingSpeed.ValueOf(sSpeed);
		
		ShootSpeed = spd ?? ShootingSpeed.SLOWEST;
		if (spd == null)
			PlayerPrefs.SetString("ShootingSpeed", ShootSpeed.ToString());

		DelayBetweenShots = ShootSpeed.Speed;
		
		PlayerPrefs.Save();
	}

	public void Update()
	{
		if (Input.GetButtonDown("Fire1"))
			heroAnimator.SetBool(IsShooting, true);

		if (Input.GetButton("Fire1"))
		{
			Shoot();
		}

		if (Input.GetButtonUp("Fire1"))
		{
			heroAnimator.SetBool(IsShooting, false);
		}

		if (Input.GetButtonDown("Blank")) {
			Player.Instance.UseBlank();
		}
	}

	private void FixedUpdate()
	{
		
	}

	private void Shoot()
	{
		if (!canShoot)
			return;

		StartCoroutine(BulletDelay());
		Quaternion rot = transform.rotation;
		GameObject left = Instantiate(bulletPrefab, leftCannon.position, rot, null);
		GameObject right = Instantiate(bulletPrefab, rightCannon.position, rot, null);

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
		while (elapsed <= DelayBetweenShots)
		{
			elapsed += Time.deltaTime;
			yield return null;
		}

		canShoot = true;
	}
}
