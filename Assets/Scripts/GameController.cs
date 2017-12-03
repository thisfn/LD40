using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public static int WinAmount = 12;
	public static Material WinnerMat;
	[SerializeField] private int _loweringFactor = 1;
	[SerializeField] private int _beeAmountToChange = 10;
	private PlayerManager _playerManager;
	private InterfaceController _interfaceController;

	private int _counter;

	private void Start()
	{
		WinAmount = 12;
		_playerManager = FindObjectOfType<PlayerManager>();
		_interfaceController = FindObjectOfType<InterfaceController>();
		_interfaceController.winAmountText.text = WinAmount.ToString();
	}

	private void Update()
	{

		switch (PlayerManager.PlayerCount)
		{
			case 1:
				_beeAmountToChange = 6;
				break;
			case 2:
				_beeAmountToChange = 8;
				break;
			case 3:
				_beeAmountToChange = 10;
				break;
			case 4:
				_beeAmountToChange = 12;
				break;

			default:
				_beeAmountToChange = 10;
				break;
		}

		if (HoneyBox.BeeCounter / _beeAmountToChange > _counter)
		{
			if (WinAmount - _playerManager.GetTopPlayer().TotalScore > 2)
			{
				_counter++;
				WinAmount -= _loweringFactor;
				_interfaceController.winAmountText.text = WinAmount.ToString();
				//TODO add an animation when changing the amount
			}
		}
	}

	public static void DefineWinner(GameObject player)
	{
		WinnerMat = player.GetComponentInChildren<SpriteRenderer>().material;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}