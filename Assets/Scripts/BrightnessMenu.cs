using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BrightnessMenu : MonoBehaviour
{
    ColorGrading col = null;

    public static float brightness_level = 1.8f;

    // Start is called before the first frame update
    void Awake()
    {
        PostProcessVolume volume = gameObject.GetComponent<PostProcessVolume>();
        // somewhere during initializing
        volume.profile.TryGetSettings(out col);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetBrightness(float brightness)
    {
        col.postExposure.value = brightness;
        brightness_level = brightness;
    }
}
