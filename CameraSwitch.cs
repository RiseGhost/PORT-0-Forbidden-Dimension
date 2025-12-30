using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private Camera[] cameras;
    private int index = 0;

    void Start()
    {
        for(short i = 1; i < cameras.Length; i++)
        {
            cameras[i].enabled = false;
        }
    }

    void Update()
    {
        if (Keyboard.current[Key.C].wasPressedThisFrame) Switch();
    }

    public void Switch()
    {
        cameras[index].enabled = false;
        index = (index + 1) % cameras.Length;
        cameras[index].enabled = true;
    }
}
