using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource Y2KSource;
    [SerializeField] AudioSource VaporwaveSource;

    [SerializeField] float volumeMultiplier = 0.5f;

    public Slider volumeSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        volumeSlider.value = volumeMultiplier;
    }

    public void SetFadeState(float fade)
    {
        Debug.Log("SETTING FADE IT'S " + fade * volumeMultiplier);
        VaporwaveSource.volume = fade * volumeMultiplier;
        Y2KSource.volume = (1 - fade) * volumeMultiplier;
    }

    public void AdjustVolume(float newVolume) {
        volumeMultiplier = newVolume;
    }
}
