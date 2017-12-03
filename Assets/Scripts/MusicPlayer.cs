using UnityEngine;

public class MusicPlayer : MonoBehaviour 
{
	AudioSource audioSource;
	[SerializeField] AudioClip[] songs;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
		audioSource.clip = songs[Random.Range(0, songs.Length)];
		audioSource.Play();
	}
}