using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public int Id;
	public int InstanceNumber;

	[SerializeField] private int _maxHp;
	[SerializeField] private int _currentHp;

	public List<Bee> Bees;
	public int Score;
	private InterfaceController _interfaceController;
	private PlayerManager _playerManager;


	private void OnEnable()
	{
		_currentHp = _maxHp;
		Score = 0;
		_interfaceController = FindObjectOfType<InterfaceController>();
		_interfaceController.UpdateText(InstanceNumber, Score);
	}

	private void Start()
	{
		_playerManager = GetComponentInParent<PlayerManager>();
		_interfaceController = FindObjectOfType<InterfaceController>();
		_currentHp = _maxHp;
		Score = 0;
		_interfaceController.UpdateText(InstanceNumber, Score);
	}

	public void AddScore()
	{
		Score++;
		_interfaceController.UpdateText(InstanceNumber, Score);
	}

	public void AddDamage()
	{
		_currentHp--;
		if (_currentHp <= 0)
		{
			var topPlayer = _playerManager.GetTopPlayer(this);

			if (topPlayer != this)
			{
				foreach (Bee bee in Bees)
				{
					Bees.Remove(bee);
					topPlayer.Bees.Add(bee);
					bee.Target = topPlayer;
				}
			}

			_playerManager.PlayerIsDead(gameObject);
			gameObject.SetActive(false);
		}
	}
}