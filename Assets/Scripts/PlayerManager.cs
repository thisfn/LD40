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
				go.GetComponent<MeshRenderer>().material = _materials[PlayerCount]; //Todo Change sprite color
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
		pgo.transform.position = _respawns[Random.Range(0, _respawns.Count)].position;
		pgo.transform.parent = transform;
	}

	public Player GetTopPlayer(Player playerDying)
	{
		GameObject temp = new GameObject();
		Player topPlayer = temp.gameObject.AddComponent<Player>();
		Player[] players = GetComponentsInChildren<Player>();

		foreach (var player in players)
		{
			if (player == playerDying)
				continue;

			if (player.Score > topPlayer.Score)
			{
				topPlayer = player;
			}
		}

		//Todo enrique vai fazer a expansao de dividir as abelhas entre o empate
		if (PlayerCount == 1 || topPlayer.Score == 0)
		{
			topPlayer = playerDying;
		}


		Destroy(temp);

		return topPlayer;
	}
}
