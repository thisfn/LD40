using UnityEngine;

public class HoneyBox : MonoBehaviour 
{
	[SerializeField] private float _collectTime;
	[SerializeField] private float _creationTime;
	[SerializeField] private float _creationCounter;
	[SerializeField] private float _collectCounter;

	[SerializeField] private bool _hasHoney;
	[SerializeField] private bool _onCollect;

	private void Update()
	{
		if (!_hasHoney)
		{
			_creationCounter += Time.deltaTime;
			if (_creationCounter > _creationTime)
			{
				_hasHoney = true;
				_creationCounter = 0;
			}
		}

		else
		{
			if (_onCollect)
			{
				_collectCounter += Time.deltaTime;

				if (_collectCounter > _collectTime)
				{
					_hasHoney = false;
					_collectCounter = 0;
					_onCollect = false;
				}
			}

			else
			{
				_collectCounter -= Time.deltaTime;

				if (_collectCounter < 0f)
				{
					_collectCounter = 0;
				}
			}
		}
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("OnTrigger Enter: " + collision.gameObject.name);
		if (!_hasHoney)
		{
			return;
		}

		if (collision.CompareTag("Player"))
		{
			_onCollect = true;
		}
	}


	private void OnTriggerExit2D(Collider2D collision)
	{
		if (!_hasHoney)
		{
			return;
		}

		if (collision.CompareTag("Player"))
		{
			_onCollect = false;
		}
	}
}