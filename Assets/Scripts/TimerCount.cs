using UnityEngine;
using UnityEngine.UI;

public class TimerCount : MonoBehaviour
{
	public float timerRestante;
	public Text timerText;
	public int timerMin;
	public int timerSeg;
	PlayerManager playerManager;
	GameController _gameController;

	private void Start()
	{
		playerManager = FindObjectOfType<PlayerManager>();
		_gameController = FindObjectOfType<GameController>();
	}

	private void Update()
	{
		if (timerRestante > 0)
		{
			timerRestante -= Time.deltaTime;
		}
		else
		{
			_gameController.DefineWinner(playerManager.GetTopPlayer().gameObject);
		}
		timerMin = (int) (timerRestante / 60f);
		timerSeg = (int) (timerRestante % 60f);
		if (timerSeg >= 10)
		{
			timerText.text = (timerMin.ToString() + ":" + timerSeg.ToString());
		}
		else
		{
			timerText.text = (timerMin.ToString() + ":0" + timerSeg.ToString());
		}
		
	}


}