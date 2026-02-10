using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[System.Serializable]
public struct CameraEntry
{
    public Camera camera;
    public bool is_Server_Select;
}

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private CameraEntry[] cameras;
    private Camera Focus_Camera;
    private int index = 0;

    void Start()
    {
        for(short i = 1; i < cameras.Length; i++)
        {
            cameras[i].camera.gameObject.SetActive(false);
        }

        List<CameraEntry> focus = cameras.Where(x => x.is_Server_Select).ToList();
        if (focus.Count == 0) return;
        Focus_Camera = focus.First().camera;
    }

    void Update()
    {
        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject != null)
            return;
        if (Keyboard.current[Key.C].wasPressedThisFrame) Switch();
    }

    public Camera Switch()
    {
        cameras[index].camera.gameObject.SetActive(false);
        index = (index + 1) % cameras.Length;
        cameras[index].camera.gameObject.SetActive(true);
        return cameras[index].camera;
    }

    public Camera Switch_Focus_Camera()
    {
        if (Focus_Camera == null) return null;
        cameras.First().camera.gameObject.SetActive(false);
        Focus_Camera.gameObject.SetActive(true);
        return Focus_Camera;
    }

    public void Switch_main_camera()
    {
        cameras.First().camera.gameObject.SetActive(true);
        if (Focus_Camera == null) return;
        Focus_Camera.gameObject.SetActive(false);
    }
}
