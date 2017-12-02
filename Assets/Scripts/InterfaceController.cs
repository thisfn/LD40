using UnityEngine;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour 
{
	[SerializeField] private Text[] _texts;

	public void UpdateText(int id, int value)
	{
		_texts[id].text = value.ToString();
	}
}