using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float _speed;
	[SerializeField] private Collider2D _attackCollider;
	private float _horizontal, _vertical;
	[SerializeField] private float _colliderDuration;
	[SerializeField] private float _cooldownDuration;
	[SerializeField] private GameObject _sphere;
	private float _colliderCounter;
	private float _cooldownCounter;
	public bool IsShielded;

	private Rigidbody2D _rb;
	private Player _player;

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

		if (_colliderCounter < 0f)
		{
			_attackCollider.enabled = false;
			_sphere.SetActive(false);
			IsShielded = false;
			_rb.mass = 1;
		}


		if (Input.GetButtonDown(("F" + _player.Id)))
		{
			if (_cooldownCounter < 0f)
			{
				_attackCollider.enabled = true;
				_sphere.SetActive(true);
				_rb.mass = 100;
				IsShielded = true;

				_cooldownCounter = _cooldownDuration;
				_colliderCounter = _colliderDuration;
			}
		}
	}
}