using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour 
{
	[SerializeField] private Material[] _materials;
	[SerializeField] private GameObject _player;
	[SerializeField] private bool[] _playersInGame;
	[SerializeField] private float _respawnDelay;

	InterfaceController _interfaceController;

	private List<Transform> _respawns;

	[SerializeField] public static int PlayerCount;

	private void Start()
	{
		_respawns = new List<Transform>();
		_interfaceController = FindObjectOfType<InterfaceController>();
		foreach (Transform child in transform)
		{
			_respawns.Add(child);
		}
	}

	private void Update()
	{
		if (PlayerCount < 4)
		{
			for (var i = 0; i < _playersInGame.Length; i++)
			{
				if (!_playersInGame[i])
				{
					VerifyPlayer(i);
				}
			}
		}
	}

	private void VerifyPlayer(int position)
	{
		if (Input.GetButtonDown("F" + position))
		{
			if (!_playersInGame[position])
			{
				var go = Instantiate(_player, _respawns[Random.Range(0, _respawns.Count)].position, Quaternion.identity, transform);
				go.name = "Player " + (PlayerCount + 1);
				go.GetComponent<Player>().Id = position;
				go.GetComponent<Player>().InstanceNumber = PlayerCount;
				go.GetComponent<Player>().PlayerScoreText = _interfaceController.Texts[PlayerCount];
				go.GetComponentInChildren<SpriteRenderer>().material = _materials[PlayerCount];
				PlayerCount++;
				_playersInGame[position] = true;
			}
		}
	}


	public void PlayerIsDead(GameObject playerGo)
	{
		StartCoroutine(ReCreate(playerGo));
	}

	private IEnumerator ReCreate(GameObject pgo)
	{
		yield return new WaitForSeconds(_respawnDelay);

		pgo.SetActive(true);
		pgo.GetComponent<PlayerMovement>().ShieldOnRespawn();
		pgo.transform.position = _respawns[Random.Range(0, _respawns.Count)].position;
		pgo.transform.parent = transform;
	}

	public Player GetTopPlayer(Player thisPlayer)
	{
		GameObject temp = new GameObject();
		Player topPlayer = temp.gameObject.AddComponent<Player>();
		Player[] players = GetComponentsInChildren<Player>();

		foreach (var player in players)
		{
			if (player.Crown != null)
				player.Crown.SetActive(false);

			if (player.TotalScore > topPlayer.TotalScore)
			{
				topPlayer = player;
			}
		}

		if (PlayerCount == 1)
		{
			topPlayer = thisPlayer;
		}

		Destroy(temp);

		if (topPlayer.TotalScore > 0)
		{
			if (topPlayer.Crown != null)
				topPlayer.Crown.SetActive(true);
		}


		string playerName;

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

		return topPlayer;
	}
}
