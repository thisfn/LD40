using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float _speed;
	[SerializeField] private Collider2D _attackCollider;
	private float _horizontal, _vertical;
	[SerializeField] private float _colliderDuration;
	[SerializeField] private float _cooldownDuration;
	private float _colliderCounter;
	private float _cooldownCounter;

	private int _hp;

	private Rigidbody2D _rb;

	private void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		_horizontal = Input.GetAxisRaw("Horizontal");
		_vertical = Input.GetAxisRaw("Vertical");

		var moveVector = new Vector2(_horizontal, _vertical).normalized;

		_rb.velocity = moveVector * _speed;

		_cooldownCounter -= Time.deltaTime;
		_colliderCounter -= Time.deltaTime;

		if (_colliderCounter < 0f)
		{
			if (_attackCollider.enabled)
			{
				_cooldownCounter = _cooldownDuration;
			}

			_attackCollider.enabled = false;
		}


		if (Input.GetButtonDown("Fire1"))
		{
			if (_cooldownCounter < 0f)
			{
				_attackCollider.enabled = true;
				_colliderCounter = _colliderDuration;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Bee"))
		{
			_hp--;
			print(_hp);
		}

	}
}