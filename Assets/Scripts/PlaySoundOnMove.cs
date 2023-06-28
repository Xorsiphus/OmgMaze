using UnityEngine;

public class PlaySoundOnMove : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip finishSound;
    public bool isFinish;

    private void OnTriggerEnter(Collider other)
    {
        if (isFinish)
        {
            audioSource.PlayOneShot(finishSound);
        }
        else
        {
            audioSource.Play();
        }
    }
}
