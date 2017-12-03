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
	public int Score;
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
		Score = 0;
	}

	private void Start()
	{
		_playerManager = GetComponentInParent<PlayerManager>();
		_currentHp = _maxHp;
		Score = 0;
		NewScore = 0;
		auxScore = 0;
	}

	private void Update()
	{

		auxScore += Score * Time.deltaTime;
		NewScore = (int) auxScore;
	

		PlayerScoreText.text = NewScore.ToString();
	}

	public void AddScore()
	{
		Score++;
		//PlayerScoreText.text = Score.ToString();
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

			_interfaceController.TopPlayer.text = topPlayer.gameObject.name;

			_playerManager.PlayerIsDead(gameObject);
			Score = 0;
			//PlayerScoreText.text = Score.ToString();
			gameObject.SetActive(false);
		}
	}
}