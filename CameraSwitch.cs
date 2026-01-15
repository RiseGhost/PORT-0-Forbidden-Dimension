using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private Camera[] cameras;
    private int index = 0;

    void Start()
    {
        for(short i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject != null)
            return;
        if (Keyboard.current[Key.C].wasPressedThisFrame) Switch();
    }

    public void Switch()
    {
        cameras[index].gameObject.SetActive(false);
        index = (index + 1) % cameras.Length;
        cameras[index].gameObject.SetActive(true);
    }
}
