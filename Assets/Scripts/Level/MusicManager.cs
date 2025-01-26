using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource Y2KSource;
    [SerializeField] AudioSource VaporwaveSource;

    [SerializeField] float volumeMultiplier = 0.5f;

    public Slider volumeSlider;

    float currentFade;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentFade = 1;
        SetVolumes();
    }

    // Update is called once per frame
    void Update()
    {
        if (volumeSlider != null)
        {
            volumeSlider.value = volumeMultiplier;
        }   
    }

    public void SetFadeState(float fade)
    {
        currentFade = fade;

        SetVolumes();
    }

    void SetVolumes()
    {
        Debug.Log("SETTING FADE IT'S " + currentFade * volumeMultiplier);
        VaporwaveSource.volume = currentFade * volumeMultiplier;
        Y2KSource.volume = (1 - currentFade) * volumeMultiplier;
    }

    public void AdjustVolume(float newVolume) {
        volumeMultiplier = newVolume;

        SetVolumes();
    }
}
