using System;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class TaskImplement : Task
{
    [SerializeField] private float Tflops = 0f;
    [SerializeField] private string Name = "";
    [SerializeField] private TaskDifficulty Difficulty = TaskDifficulty.Very_Easy;
    [SerializeField] private TaskDescription taskDescription;
    [SerializeField] private MiniGameTechnologyAreaGroup technologyGroup;
    [SerializeField] private MiniGameType miniGameType = MiniGameType.WordRush;
    private Client client = null;

    public float getTflops(){ return Tflops;}
    public virtual string getName(){ return Name; }
    public TaskDescription getTaskDescription(){ return taskDescription; }
    public TaskDifficulty getDifficulty(){ return Difficulty; }
    public MiniGame getMiniGame()
    {
        switch (miniGameType)
        {
            case MiniGameType.WordRush:
                return new WordRushImplement(taskDescription.type, technologyGroup);
            default:
                return new WordRushImplement(taskDescription.type, technologyGroup);
        }
    }

    public MiniGameTechnologyAreaGroup getTechnologyAreaGroup() { return technologyGroup; }
    public Client GetClient(){ return client; }

    virtual public void Launch(MonoBehaviour anchor)
    {
        NotificationTask noti = new NotificationTask(Name,taskDescription.description,Key.Tab,Key.X,anchor);
        Client client = ClientServer.random();
        if (client != null) this.client = client;
        noti.setTask(this);
        noti.Show();
    }
}