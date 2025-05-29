using UnityEngine;
using System.Collections;

public class LightFlickerController : MonoBehaviour
{
    [SerializeField] private Light             lamp;
    [SerializeField] private Collider          freezeZoneCollider;  // assign your FreezeZone’s BoxCollider here
    [SerializeField] private FreezeZoneTrigger freezeZoneTrigger;   // assign the same GameObject’s trigger component here
    [SerializeField] private float             duration = 10f;

    void Awake()
    {
        lamp.enabled = false;
        // start with the entire zone GameObject off
        freezeZoneCollider.gameObject.SetActive(false);
    }

    // Hook this up to your SwitchActivator → OnActivated() event
    public void StartFlicker()
    {
        lamp.enabled = true;
        // turn the whole zone back on
        freezeZoneCollider.gameObject.SetActive(true);
        StartCoroutine(FlickerAndStop());
    }

    private IEnumerator FlickerAndStop()
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            lamp.intensity = Random.Range(0f, 2f);
            yield return new WaitForSeconds(0.05f);
            elapsed += 0.05f;
        }

        // first, explicitly un-freeze anyone
        freezeZoneTrigger.ReleaseAll();

        // now turn off the light and disable the entire zone GameObject—
        // this fires FreezeZoneTrigger.OnDisable(), which also calls ReleaseAll()
        lamp.enabled = false;
        freezeZoneCollider.gameObject.SetActive(false);
    }
}
