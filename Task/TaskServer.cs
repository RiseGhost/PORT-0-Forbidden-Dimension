using System.Linq;
using UnityEngine;

public class TaskServer
{
    public TaskServer(TaskDifficulty difficulty,MonoBehaviour anchor)
    {
        Task[] data     = Resources.Load<TaskTableObject>("Task/TaskTable").getTasks();
        Task[] tasks    = data.Where(x => x.getDifficulty() == difficulty).ToArray();
        if (tasks.Length == 0) Debug.Log("TaskServer: Don't exist Tasks to Launch");
        int randomIndex = Random.Range(0,tasks.Length);
        tasks[randomIndex].Launch(anchor);
    }
}