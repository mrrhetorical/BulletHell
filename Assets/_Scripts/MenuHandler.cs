using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour {
	public static MenuHandler Instance { get; private set; }

	public GameObject gameOverMenu;

	public int menuSceneBuildId = 0;
	public int infiniteSceneBuildId = 1;
	
	private void Awake() {
		if (Instance != null) {
			if (gameOverMenu != null)
				Instance.gameOverMenu = gameOverMenu;
			Destroy(gameObject);
			return;
		}

		Instance = this;
	}

	public void EnableGameOverMenu() {
		gameOverMenu.SetActive(true);
	}

	public void RestartGame() {
		SceneManager.LoadScene(1);
		Time.timeScale = 1f;
	}

	public void StartGame() {
		SceneManager.UnloadSceneAsync(0);
		SceneManager.LoadScene(1);
		Time.timeScale = 1f;
	}

	public void GoToMenu() {
		SceneManager.UnloadSceneAsync(1);
		SceneManager.LoadScene(0);
	}

	public void QuitGame() {
		Application.Quit(0);
	}
}
