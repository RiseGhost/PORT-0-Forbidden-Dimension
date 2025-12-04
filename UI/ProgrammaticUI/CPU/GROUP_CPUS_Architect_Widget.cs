using UnityEngine;

public class GROUP_CPUS_Architect_Widget : GroupToggle<CPUArchitectStatus>
{
    [SerializeField] UI_CPU_Architect_WIDGET template;
    void Start()
    {
        foreach (var cpu in Status)
        {
            var c = Instantiate(template, transform).GetComponent<UI_CPU_Architect_WIDGET>();
            c.setStatus(cpu);
            c.setSelectColor(selectColor);
            c.setDefaultColor(defaultColor);
            c.getWidget().group = getWidget();
        }
    }

    public override CPUArchitectStatus getSelect()
    {
        for (ushort i = 0; i < transform.childCount; i++)
        {
            var cpu = transform.GetChild(i).GetComponent<UI_CPU_Architect_WIDGET>();
            if (cpu == null) continue;
            if (cpu.isSelect()) return cpu.data;
        }
        return null;
    }
}