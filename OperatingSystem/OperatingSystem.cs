using UnityEngine;
using UnityEngine.UIElements;

public enum OperatingSystemName
{
    Ubuntu,
    Fedora,
    Debian,
    KaliLinux,
    LinuxMint,
}

[System.Serializable]
public struct OperatingSystem
{
    public VisualTreeAsset DesktopLayout;
    public OperatingSystemName Name;
    public string DisplayName;
    public bool isPay;
    public Texture2D icon;
    public Color PrimaryColor;
    public Color SecondaryColor;
    public Color ThirdColor;
    public Color TextColor;
}