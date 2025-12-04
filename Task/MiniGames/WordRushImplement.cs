using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class WordRushImplement: MiniGame
{
    [SerializeField] private TeachPhrasesTableObject phrases;
    [SerializeField] private TaskType type;
    [SerializeField] private MiniGameTechnologyAreaGroup technologyGroup;

    private static bool technologyAreaContains(MiniGameTechnologyArea technologyArea)
    {

        return false;
    }

    public WordRushImplement(TaskType type, MiniGameTechnologyAreaGroup technologyGroup)
    {
        WordRushTable data = Resources.Load<WordRushTable>("Task/MiniGame/WordRush/WordRush Table");
        List<WordRushImplement> content = data.wordRushes.Where(x => 
        x.type == type 
        && x.getMiniGameTechnologyAreaGroup().ContainsAllArea(technologyGroup)).ToList();
        if (content.Count == 0)
        {
            throw new Exception("Not exist WordRushImplement in resource with type = " + type.ToString() + " technologyGroup = " + technologyGroup.ToString());
            return;
        }
        var randomIndex = UnityEngine.Random.Range(0,content.Count);
        var selectContent = content[randomIndex];
        this.type = selectContent.getTaskType();
        this.technologyGroup = selectContent.getMiniGameTechnologyAreaGroup();
        this.phrases = selectContent.phrases;
    }

    public bool isCompleted(){ return false; }
    public float getScore(){ return 0; }
    public string getName(){ return "WordRush";}
    public TaskType getTaskType() { return type; }
    public MiniGameTechnologyAreaGroup getMiniGameTechnologyAreaGroup() { return technologyGroup; }
    public void save()
    {
        throw new NotImplementedException();
    }
    public void Start()
    {
        SceneManager.LoadSceneAsync("WordRush", LoadSceneMode.Additive);
    }

    public static void Close()
    {
        SceneManager.UnloadSceneAsync("WordRush");
    }
}