using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bee : MonoBehaviour
{
	[SerializeField] private float _speed;
	[SerializeField] private float _sightRadius;
	[SerializeField] private float _attackRadius;
	[SerializeField] private LayerMask _playerMask;
	private GameObject _player;

	private bool _onSight;
	private bool _onAttack;

	private Rigidbody2D _rb;

	private void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
		_player = GameObject.FindGameObjectWithTag("Player");

	}

	private void Update()
	{
		_onSight = Physics2D.OverlapCircle(transform.position, _sightRadius, _playerMask);
		_onAttack = Physics2D.OverlapCircle(transform.position, _attackRadius, _playerMask);

		if (_onAttack)
		{
			_onSight = false;
		}

		if (_onSight)
		{
			transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, _sightRadius);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, _attackRadius);
	}
}