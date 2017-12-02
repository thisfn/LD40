using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float _speed;
	[SerializeField] private Collider2D _attackCollider;
	[SerializeField] private Collider2D _normalCollider;
	private float _horizontal, _vertical;
	[SerializeField] private float _colliderDuration;
	[SerializeField] private float _cooldownDuration;
	[SerializeField] private GameObject _sphere;
	private float _colliderCounter;
	private float _cooldownCounter;

	private Rigidbody2D _rb;
	private Player _player;
	[SerializeField] private float _normalColliderTimer;
	private float _normalColliderCountdown;

	private void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
		_player = GetComponent<Player>();
	}

	private void Update()
	{
		_horizontal = Input.GetAxisRaw("H" + _player.Id);
		_vertical = Input.GetAxisRaw("V" + _player.Id);

		var moveVector = new Vector2(_horizontal, _vertical).normalized;

		_rb.velocity = moveVector * _speed;

		_cooldownCounter -= Time.deltaTime;
		_colliderCounter -= Time.deltaTime;
		_normalColliderCountdown -= Time.deltaTime;

		if (_normalColliderCountdown <= 0f)
		{
			_normalCollider.enabled = true;
		}

		if (_colliderCounter < 0f)
		{
			_attackCollider.enabled = false;
			_sphere.SetActive(false);
		}


		if (Input.GetButtonDown(("F" + _player.Id)))
		{
			if (_cooldownCounter < 0f)
			{
				_attackCollider.enabled = true;
				_sphere.SetActive(true);

				_cooldownCounter = _cooldownDuration;
				_colliderCounter = _colliderDuration;
			}
		}
	}


	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Bee") && _attackCollider.enabled == false)
		{
			_normalCollider.enabled = false;
			_normalColliderCountdown = _normalColliderTimer;

			//TODO Reativar o collider depois de tomar dano - periodo de invulnerabilidade
			_player.AddDamage();
		}
	}
}