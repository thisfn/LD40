using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerCount : MonoBehaviour
{
	public float timerRestante;
	public Text timerText;
	public int timerMin;
	public int timerSeg;
	private void Update()
	{
		if (timerRestante > 0)
		{
			timerRestante -= Time.deltaTime;
		}
		else
		{
			SceneManager.LoadScene(0);//TODO ENRIQUE ADICIONA BEEHOLDER/TELA DE VITORIA
			//add Beeholder!!!
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