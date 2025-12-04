using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float distance = 5f;
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private float sensitivity = 1f;
    private Transform camTrans;
    private float yaw = 0f;
    private static bool RotateCam = true;

    void Awake()
    {
        camTrans = Camera.main.transform;
        UnlockRotate();
    }

    void LateUpdate()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        // Atualiza apenas a rotação horizontal
        if (RotateCam)
            yaw += ((Mathf.Abs(mouseDelta.x) > 4) ? ((mouseDelta.x > 0) ? 4 : -4) * sensitivity : mouseDelta.x * sensitivity);


        // Mantém a altura da câmara constante
        Vector3 targetPos = transform.position + Quaternion.Euler(0, yaw, 0) * new Vector3(-distance,distance * 0.5f, -distance);

        // Move suavemente a câmara
        camTrans.position = Vector3.Lerp(camTrans.position, targetPos, Time.deltaTime * followSpeed);
        camTrans.LookAt(transform.position);
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
