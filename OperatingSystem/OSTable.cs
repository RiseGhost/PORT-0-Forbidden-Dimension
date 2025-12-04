using UnityEngine;

[CreateAssetMenu(fileName = "OSTable", menuName = "ScriptTableObjects/OperationSystem/Table")]
public class OSTable : ScriptableObject
{
    public OperatingSystem[] os;

    public static OperatingSystem? getSystem(OperatingSystemName name, OSTable os)
    {
        foreach (var system in os.os)
        {
            if (system.Name.Equals(name)) return system;
        }
        return null;
    }
}