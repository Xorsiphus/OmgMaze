using UnityEngine;

public class PlaySoundOnMove : MonoBehaviour
{
    [SerializeField] private AudioSource wallSound;

    private void OnTriggerEnter(Collider other)
    {
        wallSound.Play();
    }
}
