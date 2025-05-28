using UnityEngine;
using System.Collections;

public class LightFlickerController : MonoBehaviour
{
    [SerializeField] Light     lamp;
    [SerializeField] Collider  freezeZoneCollider;
    [SerializeField] float     duration = 10f;

    void Awake()
    {
        // start dark and un‐frozen
        lamp.enabled                = false;
        freezeZoneCollider.enabled  = false;
    }

    public void StartFlicker()
    {
        lamp.enabled               = true;
        freezeZoneCollider.enabled = true;
        StartCoroutine(FlickerAndStop());
    }

    private IEnumerator FlickerAndStop()
    {
        float t = 0f;
        while (t < duration)
        {
            lamp.intensity = Random.Range(0f, 2f);
            yield return new WaitForSeconds(0.05f);
            t += 0.05f;
        }

        // turn off lamp and collider — collider.Disable will fire OnTriggerExit
        lamp.enabled               = false;
        freezeZoneCollider.enabled = false;
    }
}
