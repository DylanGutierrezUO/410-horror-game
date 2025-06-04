using UnityEngine;

[RequireComponent(typeof(Light))]
public class FlashlightToggle : MonoBehaviour
{
    private Light _light;

    void Awake()
    {
        _light = GetComponent<Light>();
        // Start flashlight off (or on, whichever you prefer)
        _light.enabled = false;
    }

    void Update()
    {
        // When player presses F, flip the light’s enabled state
        if (Input.GetKeyDown(KeyCode.F))
        {
            _light.enabled = !_light.enabled;
        }
    }
}
