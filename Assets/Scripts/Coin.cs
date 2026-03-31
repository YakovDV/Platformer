using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public void PlaySound()
    {
        _audioSource.Play();
    }
}