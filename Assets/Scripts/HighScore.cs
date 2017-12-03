using UnityEngine;

public class HighScore : MonoBehaviour 
{
	private void Start()
	{
		GetComponent<SpriteRenderer>().material = GameController.WinnerMat;
	}
}