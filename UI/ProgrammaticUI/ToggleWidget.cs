using UnityEngine;
using UnityEngine.UI;

public abstract class ToggleWidget<E> : ProgrammaticWidget<Toggle, E>
{
    private Color selectColor;
    private Color defaultColor;

    public override void setTitle(string title)
    {
        this.title = title;
        Text text = new GameObject().AddComponent<Text>();
        text.text = title;
        text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        text.color = Color.black;
        text.name = "Text";
        text.alignment = TextAnchor.MiddleCenter;
        RectTransform rect = text.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.zero;
        text.transform.SetParent(widget.transform);
    }

    public bool isSelect() { return widget.isOn; }

    public void setSelectColor(Color selectColor) { this.selectColor = selectColor; }
    public void setDefaultColor(Color defaultColor) { this.defaultColor = defaultColor; }

    void Update()
    {
        setBackground((isSelect()) ? selectColor : defaultColor);
    }
}