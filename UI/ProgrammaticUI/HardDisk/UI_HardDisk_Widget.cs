using TMPro;
using UnityEngine;

public class UI_HardDisk_Widget : ToggleWidget<HardDiscStatus>
{
    [SerializeField] private TextMeshProUGUI Name, Space, Price;

    void Start()
    {
        this.name = "HardDisk_Widget";
    }

    public void setStatus(HardDiscStatus status)
    {
        Name.text = status.GetValue().Name;
        Space.text = status.GetValue().TotalSpace.ToString();
        Price.text = status.GetValue().Price.ToString();
        this.data = status;
    }

    public override void setDescription(string description)
    {
        throw new System.NotImplementedException();
    }
}