using TMPro;
using UnityEngine;

/*
    Description:
        This class is responsible for controlling the data relating to a task that will appear in the UI.
        This UI Will used in the content od a PopUpTask
*/

public class PopContentTask : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI LabelSubtitle;
    [SerializeField] private TextMeshProUGUI LabelDescription, LabelDifficulty, LabelServiceType;
    [SerializeField] private TechnologyCardGroup technologyCardGroup;
    private Task task;

    public void setTask(Task task)
    {
        this.task = task;
        if (LabelSubtitle != null)      LabelSubtitle.text = task.getName();
        if (LabelDescription != null)   LabelDescription.text = task.getTaskDescription().description;
        if (LabelDifficulty != null)    LabelDifficulty.text = task.getDifficulty().ToString();
        if (LabelServiceType != null)   LabelServiceType.text = task.getTaskDescription().type.ToString();

        if (technologyCardGroup != null)    technologyCardGroup.setTechnologyAreaGroup(task.getTechnologyAreaGroup());
    }
}