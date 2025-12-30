using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BtnInstallOS : MonoBehaviour
{
    [SerializeField] private Install_OS_UI InstallUI;
    [SerializeField] private Key AcceptKey = Key.Tab;
    private Slider slider;
    private float value = 0f;
    
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    void FixedUpdate()
    {
        if (Keyboard.current[AcceptKey].isPressed) value += 0.5f;
        else value -= 0.5f;
        if (value > 100f) value = 100f;
        if (value < 0f) value = 0f;
        slider.value = value;
        if (slider.value >= 99f)
        {
            if (InstallUI == null) Destroy(transform.parent.gameObject);
            Instantiate(InstallUI,Vector2.zero,Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
