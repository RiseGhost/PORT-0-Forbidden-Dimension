using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MotherBoard_Widget : ToggleWidget<MotherBoardStatus>
{
    [SerializeField] private TextMeshProUGUI Model, Mark, Price, DisckNumber;
    [SerializeField] private RawImage icon;

    public override void setDescription(string description)
    {
        throw new System.NotImplementedException();
    }

    public void setStatus(MotherBoardStatus status)
    {
        MotherBoard motherBoard = status.GetValue();
        Model.text = motherBoard.Model;
        Mark.text = motherBoard.motherBoardMark.ToString();
        Price.text = motherBoard.Price.ToString() + " €";
        DisckNumber.text = motherBoard.MaxDisk.ToString();
        data = status;
    }
}