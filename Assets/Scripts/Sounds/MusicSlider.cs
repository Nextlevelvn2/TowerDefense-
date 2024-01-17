using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MusicSlider : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    private void Start()
    {
        musicSlider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeMusicVolume(val));
    }
}
