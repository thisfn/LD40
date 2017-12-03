using UnityEngine;

//[RequireComponent(typeof(SpriteRenderer))]
public class HoneyBox : MonoBehaviour
{
	[SerializeField] private float _honeyTime;
	[SerializeField] private float _collectTime;
	private float _honeyCounter;
	private float _collectCounter;


	public static int BeeCounter;


	[SerializeField] private Sprite _empty;
	[SerializeField] public Sprite _full;
	public SpriteRenderer _spriteRenderer;

	[SerializeField] private GameObject _bee;

	private Player _player;
	private bool _hasHoney;
	private bool _isCollecting;

	private void Start()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_honeyTime = Random.Range(_honeyTime, _honeyTime * 2f);
	}

	private void Update()
	{
		if (!_hasHoney)
		{
			GenerateHoney();
			return;
		}

		Collect();
	}

	private void GenerateHoney()
	{
		_honeyCounter += Time.deltaTime;
		if (_honeyCounter > _honeyTime)
		{
			_hasHoney = true;
			_honeyCounter = 0;
			_spriteRenderer.sprite = _full;
		}
	}

	private void Collect()
	{
		if (_isCollecting)
		{
			_collectCounter += Time.deltaTime;
			if (_collectCounter > _collectTime) //Honey has been collected
			{


				_collectCounter = 0;
				_isCollecting = false;
				_hasHoney = false;
				_spriteRenderer.sprite = _empty;


				var bee = Instantiate(_bee);
				BeeCounter++;
				bee.transform.position =  new Vector3(transform.position.x, transform.position.y, bee.transform.position.z);
				bee.GetComponent<Bee>().Target = _player;
				_player.Bees.Add(bee.GetComponent<Bee>());
				_player.AddScore(1);

			}

			return;
		}

		_collectCounter -= Time.deltaTime;
		if (_collectCounter < 0f)
		{
			_collectCounter = 0f;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && _hasHoney)
		{
			_isCollecting = true;
			_player = collision.gameObject.GetComponent<Player>();
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			_isCollecting = false;
			_player = null;
		}
	}
}