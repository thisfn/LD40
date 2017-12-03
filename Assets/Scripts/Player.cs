using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	public int Id;
	public int InstanceNumber;

	[SerializeField] private int _maxHp;
	[SerializeField] private int _currentHp;
	public Text PlayerScoreText;

	[SerializeField] public GameObject Crown;

	public List<Bee> Bees;
	public int HoneyPerSecond;
	public int TotalScore;
	private PlayerManager _playerManager;

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
		PlayerScoreText.color = GetComponentInChildren<SpriteRenderer>().material.color;
		PlayerScoreText.text = TotalScore.ToString();
	}

	public void AddScore()
	{
		TotalScore++;
		PlayerScoreText.text = TotalScore.ToString();
		_playerManager.GetTopPlayer(this);
	}

	public void AddDamage()
	{
		_currentHp--;
		if (_currentHp <= 0)
		{
			TotalScore = 0;
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

			_playerManager.PlayerIsDead(gameObject);
			PlayerScoreText.text = TotalScore.ToString();
			gameObject.SetActive(false);
		}
	}
}