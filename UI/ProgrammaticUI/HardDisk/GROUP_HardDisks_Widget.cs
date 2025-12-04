using System.Collections.Generic;
using UnityEngine;

public class GROUP_HardDisks_Widget : GroupToggle<HardDiscStatus>
{
    [SerializeField] private UI_HardDisk_Widget template;

    void Start()
    {
        foreach(HardDiscStatus s in Status)
        {
            UI_HardDisk_Widget HardDisk_Widget = Instantiate(template, transform);
            HardDisk_Widget.setStatus(s);
            HardDisk_Widget.getWidget().group = getWidget();
            HardDisk_Widget.setSelectColor(selectColor);
            HardDisk_Widget.setDefaultColor(defaultColor);
        }
    }

    public override void setDescription(string description)
    {
        base.setDescription(description);
    }

    public override HardDiscStatus getSelect()
    {
        foreach (Transform child in transform)
        {
            UI_HardDisk_Widget disk = child.gameObject.GetComponent<UI_HardDisk_Widget>();
            if (disk == null) continue;
            if (disk.isSelect()) return disk.getData();
        }
        return null;
    }
}