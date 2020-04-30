using System;
using UnityEngine;

public class MouseController : MonoBehaviour
{
	private static MouseController instance;

	private Camera cam;
	[SerializeField] private Transform cursor;

	public bool defaultVisibility = false;
	
	private void Start()
	{
		if (instance != null)
			return;

		instance = this;

		cam = FindObjectOfType<Camera>();
		
		SetVisibility(defaultVisibility);
		
	}

	public static MouseController GetInstance()
	{
		return instance;
	}

	public void Update()
	{
		if (cursor == null || Cursor.visible)
			return;

		Transform t = cursor.transform;
		Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
		pos.z = 0;
		t.position = pos;
	}

	public void ToggleVisibility()
	{
		Cursor.visible = !Cursor.visible;
	}

	public void SetVisibility(bool vis)
	{
		Cursor.visible = vis;
	}
}