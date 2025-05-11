using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerInteraction : MonoBehaviour
{
    public float interactRange = 5f;
    private Interactable currentFocus;
    private Camera cam;

    void Start() => cam = GetComponent<Camera>();

    void Update()
    {
        // shoot a ray from center of screen
        var ray = cam.ScreenPointToRay(
          new Vector3(Screen.width/2f, Screen.height/2f));
        if (Physics.Raycast(ray, out var hit, interactRange))
        {
            var ia = hit.collider.GetComponent<Interactable>();
            if (ia != null)
            {
                if (ia != currentFocus)
                {
                    currentFocus?.OnDefocus();
                    currentFocus = ia;
                    currentFocus.OnFocus();
                }
                if (Input.GetKeyDown(KeyCode.E))
                    currentFocus.Interact();
                return;
            }
        }
        if (currentFocus != null)
        {
            currentFocus.OnDefocus();
            currentFocus = null;
        }
    }
}
