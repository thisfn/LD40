using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	public int Id;
	public int InstanceNumber;

	[SerializeField] private int _maxHp;
	[SerializeField] private int _currentHp;
	public Text PlayerScoreText;

	public List<Bee> Bees;
	public int HoneyPerSecond;
	public float TotalScore;
	public int NewScore;
	public float auxScore;
	private InterfaceController _interfaceController;
	private PlayerManager _playerManager;

	private void Awake()
	{
		_interfaceController = FindObjectOfType<InterfaceController>();
	}

	private void OnEnable()
	{
		_currentHp = _maxHp;
		TotalScore = 0;
	}

	private void Start()
	{
		_playerManager = GetComponentInParent<PlayerManager>();
		_currentHp = _maxHp;
		TotalScore = 0;
		NewScore = 0;
		auxScore = 0;
		PlayerScoreText.color = GetComponentInChildren<SpriteRenderer>().material.color;
	}

	private void Update()
	{

		TotalScore += HoneyPerSecond * Time.deltaTime;

		NewScore = (int) TotalScore;
		PlayerScoreText.text = NewScore.ToString();
	}

	public void AddScore()
	{
		HoneyPerSecond++;
		//PlayerScoreText.text = HoneyPerSecond.ToString();
	}

	public void AddDamage()
	{
		_currentHp--;
		if (_currentHp <= 0)
		{
			var topPlayer = _playerManager.GetTopPlayer(this);
			List<Bee> tempList = new List<Bee>();


			tempList = Bees;

			if (topPlayer != this)
			{
				Bees = new List<Bee>();

				foreach (Bee bee in tempList)
				{
					bee.Target = topPlayer;
				}

				topPlayer.Bees.AddRange(tempList);
			}

			string playerName;

			//CRIAN�AS, NAO FA�AM ISSO EM CASA, AINDA MAIS QUANDO VCS TEM QUE DIVULGAR O SEU CODIGO JUNTO COM O JOGO.
			//SERIO!
			switch (topPlayer.InstanceNumber)
			{
				case 0:
					playerName = "Orange";
					break;
				case 1:
					playerName = "Green";
					break;
				case 2:
					playerName = "Blue";
					break;
				case 3:
					playerName = "Pink";
					break;
				default:
					playerName = "Deupau";
					break;
			}

			_interfaceController.TopPlayer.text = playerName;

			_playerManager.PlayerIsDead(gameObject);
			TotalScore = 0;
			//PlayerScoreText.text = HoneyPerSecond.ToString();
			gameObject.SetActive(false);
		}
	}
}