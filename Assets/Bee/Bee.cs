using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bee : MonoBehaviour
{
	[SerializeField] private float _speed;
	[SerializeField] private float _attackSpeed;
	[SerializeField] private float _sightRadius;
	[SerializeField] private float _attackRadius;
	[SerializeField] private LayerMask _playerMask;
	private Animator _anim;
	private GameObject _player;
	private Vector2 _attackArea;

	private bool _onSight;
	private bool _onAttackRange;
	private bool _attacking;

	private Rigidbody2D _rb;

	private void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
		_anim = GetComponent<Animator>();
		_player = GameObject.FindGameObjectWithTag("Player");

	}

	private void Update()
	{
		_onSight = Physics2D.OverlapCircle(transform.position, _sightRadius, _playerMask);
		_onAttackRange = Physics2D.OverlapCircle(transform.position, _attackRadius, _playerMask);

		if (_attacking)
		{
			_onSight = false;
			_onAttackRange = false;

			transform.position = Vector2.MoveTowards(transform.position, _attackArea, _attackSpeed * Time.deltaTime);
			if (Vector2.Distance(transform.position, _attackArea) < Mathf.Epsilon)
			{
				_attacking = false;
			}
		}

		else if (_onAttackRange)
		{
			_onSight = false;
			_attackArea = _player.transform.position + (_player.transform.position - transform.position);
			_attacking = true;

		}

		else if (_onSight)
		{
			transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
		}

		else
		{
			_anim.Play("Bee_Idle");
		}

		_anim.StopPlayback();

	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, _sightRadius);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, _attackRadius);
	}
}