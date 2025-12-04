using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CommandGameUI : MonoBehaviour
{
    public CommandWords commandWord;
    public OSTable osTable;
    [SerializeField] private OperatingSystemName operatingSystemName;
    [SerializeField] private Texture2D[] DestroySprites;
    private List<string> inputs = new List<string>();
    private VisualElement root;
    private Label inputCommands;
    private Label scoreLabel;
    private float currentScore = 0;

    void Awake()
    {
        commandWord.init();
    }

    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        if (root == null) Destroy(this.gameObject);
        inputCommands = root.Q<Label>("Inputs");
        scoreLabel = root.Q<Label>("Score");
        commandWord.setVisualRoot(root);
        ApplyOSColorTheme();
        ApplyDesktopTheme();
        cleanInputs();
        StartCoroutine(SpawnPhasers());
    }

    void LateUpdate()
    {
        UpdateDisplay();
        switch (commandWord.CheckCurrentWord(inputs.ToArray()))
        {
            case CommandState.Complete:
                cleanInputs();
                break;
            case CommandState.Correct:
                inputs.Add("");
                break;
            default:
                break;
        }
        currentScore = Mathf.Lerp(currentScore, commandWord.getScore(), Time.deltaTime * 2.5f);
        ApplyOSColorTheme();
        scoreLabel.text = Math.Round(currentScore).ToString();
        root.Query<VisualElement>(className: "DestroyWordAnimation")
        .Where(x => !x.ClassListContains("Animating"))
        .ToList()
        .ForEach(
            x =>
            {
                x.AddToClassList("Animating");
                StartCoroutine(DestroyAnimation(x));
            }
        );
    }

    void Update()
    {
        string current_block = inputs[inputs.Count() - 1];
        for (Key key = Key.Period; key < Key.Z; key++)
        {
            if (Keyboard.current[key].wasPressedThisFrame)
                current_block += (key == Key.Period) ? "." : key.ToString().ToLower();
        }
        if (Keyboard.current[Key.Backspace].wasReleasedThisFrame)
        {
            if (current_block.Length > 0) current_block = current_block.Substring(0, current_block.Length - 1);
        }
        inputs[inputs.Count() - 1] = current_block;
        commandWord.MoveCards(Time.deltaTime, current_block);
    }

    private void UpdateDisplay()
    {
        inputCommands.text = "";
        foreach (var command in inputs)
        {
            inputCommands.text += command + " ";
        }
    }

    private void cleanInputs() { inputs.Clear(); inputs.Add(""); }

    private IEnumerator SpawnPhasers()
    {
        while (!commandWord.CompleteLevel())
        {
            commandWord.SpawnWord();
            yield return new WaitForSeconds(2f);
        }
    }

    private void ApplyOSColorTheme()
    {
        var os = OSTable.getSystem(operatingSystemName, osTable);
        if (os == null) Destroy(this.gameObject);
        OperatingSystem operatingSystem = (OperatingSystem)os;
        Label scoreL = root.Q<Label>("Score");
        root.Q<VisualElement>("HistoryTittleBar").style.backgroundColor = operatingSystem.SecondaryColor;
        root.Q<VisualElement>("TerminalTittleBar").style.backgroundColor = operatingSystem.SecondaryColor;
        root.Q<VisualElement>("TerminalBackground").style.backgroundColor = operatingSystem.PrimaryColor;
        scoreL.style.color = (Int16.Parse(scoreL.text) != commandWord.getScore()) ? operatingSystem.ThirdColor : operatingSystem.TextColor;
        root.Q<ScrollView>("History").style.backgroundColor = operatingSystem.PrimaryColor;
        inputCommands.style.backgroundColor = operatingSystem.PrimaryColor;
        inputCommands.style.color = operatingSystem.TextColor;
    }

    private void ApplyDesktopTheme()
    {
        var os = OSTable.getSystem(operatingSystemName, osTable);
        if (os == null) Destroy(this.gameObject);
        OperatingSystem operatingSystem = (OperatingSystem)os;
        VisualElement dekstop = operatingSystem.DesktopLayout.CloneTree();
        dekstop.style.minWidth = new StyleLength(new Length(100, LengthUnit.Percent));
        dekstop.style.minHeight = new StyleLength(new Length(100, LengthUnit.Percent));
        root.Q<VisualElement>("Desktop").Add(dekstop);
    }

    public void setSystemName(OperatingSystemName name)
    {
        this.operatingSystemName = name;
        ApplyOSColorTheme();
        ApplyDesktopTheme();
    }

    private IEnumerator DestroyAnimation(VisualElement visualElement)
    {
        foreach (Texture2D texture in DestroySprites)
        {
            yield return null;
            visualElement.style.backgroundImage = new StyleBackground(texture);
        }
        root.Q<VisualElement>("SpawnWords").Remove(visualElement);
        //root.Remove(visualElement);
    }
}
