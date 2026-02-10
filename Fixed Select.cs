using UnityEngine;
using UnityEngine.EventSystems;

/*
 * 
 */

public class FixedSelect : MonoBehaviour
{
    private ushort defaultIndex = 0;
    void LateUpdate()
    {
        if (transform.childCount == 0) return;
        EventSystem current = EventSystem.current;
        if (current == null) return;
        GameObject selected = current.currentSelectedGameObject;
        foreach (Transform child in transform)
        {
            if (selected == child.gameObject && selected != null)
                return;
        }
        Transform focus = (defaultIndex < 0 || defaultIndex >= transform.childCount) ? transform.GetChild(0) : transform.GetChild(defaultIndex);
        if (!focus.gameObject.activeSelf) focus = transform.GetChild(0);
        current.SetSelectedGameObject(focus.gameObject);
    }

    public void setDefaultIndex(ushort index)
    {
        defaultIndex = index;
    }
}
