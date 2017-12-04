using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour 
{

	public void LoadNextScene()
	{
		HoneyBox.BeeCounter = 0;
		PlayerManager.PlayerCount = 0;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void LoadMenu()
	{
		PlayerManager.PlayerCount = 0;
		HoneyBox.BeeCounter = 0;
		SceneManager.LoadScene(0);
	}
}