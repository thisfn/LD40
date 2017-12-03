using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour 
{

	public void LoadNextScene()
	{
		PlayerManager.PlayerCount = 0;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}