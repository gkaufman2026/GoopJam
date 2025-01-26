using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField] AudioSource mainSource;
    [SerializeField] AudioSource footstepSource;

    [SerializeField] AudioClip jumpClip;
    [SerializeField] AudioClip swapGravity;

    [SerializeField] AudioClip shootClip;
    [SerializeField] AudioClip cantShootClip;
    [SerializeField] AudioClip reloadClip;
    [SerializeField] float reloadDelay = 0.2f;
    [SerializeField] AudioClip landClip;
    [SerializeField] AudioClip dashClip;

    [SerializeField] List<AudioClip> footstepList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainSource = GetComponent<AudioSource>();
    }

    public void JumpEvent()
    {
        ActivateSound(jumpClip);
    }

    public void FootstepEvent()
    {
        footstepSource.PlayOneShot(footstepList[Random.Range(0, footstepList.Count)]);
    }

    public void GravityEvent()
    {
        ActivateSound(swapGravity);
    }

    public void ShootEvent()
    {
        ActivateSound(shootClip);
        Invoke("ReloadEvent", shootClip.length + reloadDelay);
    }

    public void CantShootEvent()
    {
        ActivateSound(cantShootClip);
    }

    void ReloadEvent()
    {
        ActivateSound(reloadClip);
    }

    public void LandEvent()
    {
        footstepSource.PlayOneShot(landClip);
    }

    public void DashEvent()
    {
        ActivateSound(dashClip);
    }

    void ActivateSound(AudioClip sound)
    {
        if (sound == null)
        {
            Debug.LogWarning("SOUND CLIP NOT IMPLEMENTED!");
            return;
        }

        mainSource.PlayOneShot(sound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
