using TMPro;
using UnityEngine;

public class ServerTaskButton : MonoBehaviour
{
    private ServerGameObject server = null;
    private ServerChoiceCamera camera;
    [SerializeField] private TextMeshProUGUI LabelName;

    private void Update()
    {
        if (server == null) return;
        if (LabelName != null) LabelName.text = server.server.serverStatus.HostName;
    }

    public void setServer(ServerGameObject server, ServerChoiceCamera camera)
    {
        this.server = server;
        this.camera = camera;
    }
}