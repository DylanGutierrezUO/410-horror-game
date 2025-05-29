using UnityEngine;
using TMPro; // remove this when raycast is fixed

public class Note : MonoBehaviour
{
    [TextArea(3, 10)]
    public string message = "Default note text.";
}