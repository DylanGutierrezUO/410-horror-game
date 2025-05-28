using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
  public float interactRange = 3f;
  public TextMeshProUGUI promptUI;  // drag your TMP “Press E” TEXT here

  IActivatable currentTarget;

  void Update()
  {
    // raycast
    var ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
    if (Physics.Raycast(ray, out var hit, interactRange))
    {
      var a = hit.collider.GetComponent<IActivatable>();
      if (a != null)
      {
        currentTarget = a;
        promptUI.text = a.InteractionPrompt;
        promptUI.gameObject.SetActive(true);

        if (Input.GetKeyDown(KeyCode.E))
        {
          a.OnActivate();
          promptUI.gameObject.SetActive(false);
          currentTarget = null;
        }
        return;
      }
    }

    // nothing interactive
    currentTarget = null;
    promptUI.gameObject.SetActive(false);
  }
}
