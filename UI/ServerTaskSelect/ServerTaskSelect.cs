using System.Linq;
using UnityEngine;

[System.Serializable]
public struct ServerChoiceCamera
{
    public float distance;
    public float height;
    public Vector3 angle;
}

public class ServerTaskSelect : MonoBehaviour
{
    private ServerGameObject[] servers;
    [SerializeField] private ServerTaskButton button_template;
    [SerializeField] private GameObject Buttons_Container;
    [SerializeField] private ServerChoiceCamera camera;

    void Start()
    {
        if (button_template == null || Buttons_Container == null) Destroy(gameObject);
        ServerGameObject[] s = FindObjectsByType<ServerGameObject>(FindObjectsSortMode.None);
        servers = s.Where(x => x.server.serverStatus.isOperational()).ToArray();
        foreach (var server in servers)
        {
            Transform container = Buttons_Container.transform;
            ServerTaskButton button = Instantiate(button_template,container).GetComponent<ServerTaskButton>();
            button.setServer(server,camera);
        }
    }
}
