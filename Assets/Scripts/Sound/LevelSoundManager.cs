using UnityEngine;
using static UnityEngine.SpriteMask;

public class LevelSoundManager : MonoBehaviour
{
    [SerializeField] AudioSource source;

    [SerializeField] AudioClip restartClip;
    [SerializeField] AudioClip startClip;
    [SerializeField] AudioClip successClip;

    public void RestartSound()
    {
        ActivateSound(restartClip);
    }

    public void StartSound()
    {
        ActivateSound(startClip);
    }

    public void SuccessSound()
    {
        ActivateSound(successClip);
    }

    void ActivateSound(AudioClip sound)
    {
        if (sound == null)
        {
            Debug.LogWarning("SOUND CLIP NOT IMPLEMENTED!");
            return;
        }

        source.PlayOneShot(sound);
    }
}
