using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CPU_WIDGET : ToggleWidget<ProcessorStatus>
{
    [SerializeField] private TextMeshProUGUI CPUName, Architect, Mark;
    [SerializeField] private RawImage icon;
    private ProcessorStatus status;

    public override void setDescription(string description)
    {
        throw new System.NotImplementedException();
    }

    public void setStatus(ProcessorStatus status)
    {
        this.status = status;
        CPUName.text = status.GetValue();
        Architect.text = status.getArchitect().GetValue().ToString();
        Mark.text = status.getMark().GetValue().ToString();
        this.data = status;
    }
}