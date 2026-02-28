using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class EconomicLabel : MonoBehaviour
{
    [SerializeField] private PayGroup payGroup;
    private TextMeshProUGUI text;
    
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        var promises = MoneyBank.GetPromises().Where(promise => promise.group == payGroup).ToList();
        float StartValue = promises.Select(x => x.StartValue).Sum();
        text.text = StartValue.ToString();
    }
}
