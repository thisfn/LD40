using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour 
{
	[SerializeField] private Material[] _materials;
	[SerializeField] private GameObject _player;
	[SerializeField] private bool[] _playersInGame;
	[SerializeField] private float _respawnDelay;

	private Transform[] _respawns;

	public static int PlayerCount;

	private void Start()
	{
		_respawns = GetComponentsInChildren<Transform>();
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
				var go = Instantiate(_player, _respawns[Random.Range(0, _respawns.Length)].position, Quaternion.identity, transform);
				go.GetComponent<Player>().Id = position;
				go.GetComponent<Player>().InstanceNumber = PlayerCount;
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
		pgo.transform.position = _respawns[Random.Range(0, _respawns.Length)].position;
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

		if (PlayerCount == 1)
		{
			topPlayer = playerDying;
		}


		Destroy(temp);

		return topPlayer;
	}
}
