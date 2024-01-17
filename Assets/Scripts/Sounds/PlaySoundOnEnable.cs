using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnEnable : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    private void OnEnable()
    {
        SoundManager.Instance.PlaySound(clip);
    }
    public void PlaySoundUpgrade()
    {
        SoundManager.Instance.PlaySound(clip);
    }
}
