using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float distance = 5f;
    [SerializeField] private float min_zoom = 2f;
    [SerializeField] private float max_zoom = 15f;
    [SerializeField] private float minPitch = -30f;
    [SerializeField] private float maxPitch = 60f;
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private float sensitivity = 0.2f;
    [SerializeField] private InputAction scrollAction;
    [SerializeField] private float zoom_sensitivity = 5f;
    [SerializeField] private float zoom = 6f;
    private Transform camTrans;
    private float yaw = 0f;
    private Vector3 FollowPos;
    private static bool RotateCam = true;

    void Awake()
    {
        camTrans = Camera.main.transform;
        UnlockRotate();
        FollowPos = transform.position;
        scrollAction?.Enable();
    }

    void OnEnable()
    {
        scrollAction?.Enable();
    }

    void OnDisable()
    {
        scrollAction?.Disable();
    }

    void Update()
    {
        if (scrollAction == null) return;
        Vector2 scroll = scrollAction.ReadValue<Vector2>();
        zoom += scroll.y * Time.deltaTime * zoom_sensitivity;
        GameObject.FindGameObjectWithTag("Teste").GetComponent<TextMeshProUGUI>().text = scroll.ToString();
        if (distance < min_zoom) zoom = min_zoom;
        if (distance > max_zoom) zoom = max_zoom;
        Camera.main.orthographicSize = zoom;
    }

    void LateUpdate()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        // Atualiza apenas a rotação horizontal
        if (RotateCam)
            yaw += mouseDelta.x * sensitivity;

        // Move suavemente a câmara
        camTrans.parent.position = transform.position + Quaternion.Euler(0,yaw,0) * new Vector3(-distance,distance * 0.7f,-distance);
        FollowPos = Vector3.Lerp(FollowPos,transform.position,Time.deltaTime * followSpeed);
        camTrans.LookAt(FollowPos);
    }

    public static void LockRotate()
    {
        RotateCam = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public static void UnlockRotate()
    {
        RotateCam = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
