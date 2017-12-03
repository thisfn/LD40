using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerCount : MonoBehaviour
{
	public float timerRestante;
	public Text timerText;
	public int timerMin;
	public int timerSeg;
	PlayerManager playerManager;

	private void Start()
	{
		playerManager = FindObjectOfType<PlayerManager>();
	}

	private void Update()
	{
		if (timerRestante > 0)
		{
			timerRestante -= Time.deltaTime;
		}
		else
		{
			GameController.DefineWinner(playerManager.GetTopPlayer().gameObject);
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