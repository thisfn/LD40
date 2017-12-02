using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bee : MonoBehaviour
{
	[SerializeField] private float _speed;
	[SerializeField] private float _waitInterval;
	[SerializeField] private float _attackSpeed;
	[SerializeField] private float _attackDuration;
	private float _attackCounter;

	[HideInInspector] public Player Target;

	private Rigidbody2D _rb;
	private Vector3 _attackVector;


	private void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
		_rb.velocity = Vector2.zero;
	}

	private void Update()
	{
		_waitInterval -= Time.deltaTime;
		if (_waitInterval < 0f)
		{
			_attackCounter -= Time.deltaTime;
			if (_attackCounter < 0f)
			{
				_rb.velocity = Vector2.zero;
				transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, _speed * Time.deltaTime);
			}
		}
	}

	private void AttackPlayer()
	{
		_attackVector = (Target.transform.position - transform.position).normalized;
		_rb.velocity = _attackVector * _attackSpeed;
		_attackCounter = _attackDuration;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject == Target.gameObject && (_waitInterval < 0f))
		{
			AttackPlayer();
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