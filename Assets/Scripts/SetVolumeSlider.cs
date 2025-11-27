using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetVolumeSlider : MonoBehaviour
{
    [SerializeField]
    public Slider slider_vol;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slider_vol.value = MainAudioManager.audio_level * 100.0f;
    }
}
