using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bee : MonoBehaviour
{
	[SerializeField] private float _speed;
	[SerializeField] private float _waitInterval;
	[SerializeField] private float _attackSpeed;
	[SerializeField] private float _attackDuration;
	[SerializeField] private float _attackRadius = 12f;
	[SerializeField] private LayerMask _playerMask;
	[SerializeField] private float _hitTimer;
	private float _hitCounter;
	private float _attackCounter;
	public bool attackPlayer;
	private bool _isAttacking;

	private SpriteRenderer _spriteRenderer;

	[HideInInspector] public Player Target;

	private Rigidbody2D _rb;
	private Vector3 _attackVector;


	private void Start()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_rb = GetComponent<Rigidbody2D>();
		_rb.velocity = Vector2.zero;
	}

	private void Update()
	{

		_waitInterval -= Time.deltaTime;
		if (_waitInterval < 0f)
		{

			_hitCounter -= Time.deltaTime;
			attackPlayer = Physics2D.OverlapCircle(transform.position, _attackRadius, _playerMask);

			if (attackPlayer && !_isAttacking)
			{
				AttackPlayer();
			}

			_attackCounter -= Time.deltaTime;
			if (_attackCounter < 0f)
			{
				_rb.velocity = Vector2.zero;

				_spriteRenderer.flipX = (Target.transform.position - transform.position).x >= 0f;
				transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, _speed * Time.deltaTime);
				_isAttacking = false;
			}
		}
	}

	private void AttackPlayer()
	{
		_isAttacking = true;
		_attackVector = (Target.transform.position - transform.position).normalized;
		_rb.velocity = _attackVector * _attackSpeed;
		_attackCounter = _attackDuration;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player") && !collision.gameObject.GetComponent<PlayerMovement>().IsShielded)
		{
			if (_hitCounter < 0f)
			{
				collision.gameObject.GetComponent<Player>().AddDamage();
				_hitCounter = _hitTimer;
			}
		}
	}


	private void OnDrawGizmos()
	{
		//Gizmos.color = Color.yellow;
		//Gizmos.DrawWireSphere(transform.position, _sightRadius);
		//Gizmos.color = Color.red;
		//Gizmos.DrawWireSphere(transform.position, _attackRadius);
	}
}